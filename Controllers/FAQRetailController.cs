using AbleCommerce.Code;
namespace FAQPlugin.Controllers
{
    using CommerceBuilder.Common;
    using CommerceBuilder.Products;
    using CommerceBuilder.Web.Mvc;
    using global::FAQPlugin.Models;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Utility;
    using RegisterWidget = CommerceBuilder.Web.RegisterWidget;
    using Settings = Models.FAQSettings;
    using WidgetCategory = CommerceBuilder.CMS.WidgetCategory;

    public class FAQRetailController : AbleController
    {
        private readonly IProductRepository _productRepo;
        private readonly IFAQRepository _faqRepository;
        public FAQRetailController(IProductRepository productRepo, IFAQRepository faqRepository)
        {
            _productRepo = productRepo;
            _faqRepository = faqRepository;
        }

        [RegisterWidget(DisplayName = "Questions About This Product", Category = WidgetCategory.Product, Description = "Displays the most frequently asked questions related with the product.")]
        public ActionResult FAQWidget(FAQParams parameters, int page = 1, int size = 5, int productId = 0)
        {
            IList<FAQ> questions = new List<FAQ>();
            int count;
            parameters.ProductId  = PageHelper.GetProductId();
            int pageIndex = page - 1;
            int startIndex = (size * pageIndex);
            if (parameters.ProductId == 0 && productId == 0)
            {
                // if product id is zero , widget was placed at a non product page so we should show all questions
                questions = _faqRepository.LoadQuestions();
                count = questions.Count;
            }
            else
            {
                questions = _faqRepository.LoadQuestions(QuestionType.Answered, size, startIndex, SortExpression.LastModified, parameters.ProductId);
                count = _faqRepository.GetCount(QuestionType.Answered, parameters.ProductId);
            }
            var storeSettings = AbleContext.Current.Store.Settings;
            IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();
            AskQuestionModel askQuestionModel = new AskQuestionModel();
            askQuestionModel.ProductId = parameters.ProductId;

            var currentSettings = new Settings()
            {
                AllowAnonymousUsers = storeSettings.GetValueByKey("FAQ_AllowAnonymousUsers") == "True",
                DefaultResponderName = storeSettings.GetValueByKey("FAQ_DefaultResponderName")

            };

            foreach (var faq in questions)
            {
                var model = new QuestionsViewModel();
                model.FAQId = faq.Id;
                model.Question = faq.Question;
                model.Answer = faq.Answer;
                model.ProductName = faq.Product.Name;
                model.AskedBy = Utility.GetUserName(faq.User);
                model.AskedAt = Utility.TimeAgo(faq.CreatedDate);
                model.Responder = faq.AnswerBy;
                questionsList.Add(model);
            }

            StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, page, size, count);
            ViewBag.ProductId = parameters.ProductId;
            RetailViewModel viewModel = new RetailViewModel()
            {
                PagedList = pagedList,
                AskQuestionModel = askQuestionModel,
                CurrentSettings = currentSettings
            };
            return PartialView("~/Plugins/FAQPlugin/Views/_FAQWidget.cshtml", viewModel);
        }
        public ActionResult SubmitQuestion(AskQuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Input Field Cannot be Empty" });
            }
            
            var user = AbleContext.Current.User;
            var storeSettings = AbleContext.Current.Store.Settings;
            var createdDate = DateTime.Now;
            var product = _productRepo.Load(model.ProductId);
                var faq = new FAQ()
                {
                    Question = model.Question,
                    Product = product,
                    User = user,
                    CreatedDate = createdDate,
                    LastModified = createdDate,
                    IsAnswered = false,
                    Visibility = true,
                };
              _faqRepository.Save(faq);
            return Json(new { success = true });
        }
    }
}
