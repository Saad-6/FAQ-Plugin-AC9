using AbleCommerce.Code;
namespace FAQPlugin.Controllers
{
    using CommerceBuilder.Common;
    using CommerceBuilder.Products;
    using CommerceBuilder.Web.Mvc;
    using global::FAQPlugin.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using RegisterWidget = CommerceBuilder.Web.RegisterWidget;
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

        [RegisterWidget(DisplayName = "Frequently Asked Questions", Category = WidgetCategory.Product, Description = "Displays the most frequently asked questions related with the product.")]
        public ActionResult FAQWidget(FAQParams parameters)
        {
            IList<FAQ>questions = new List<FAQ>();
            Product product = new Product(); 
            parameters.ProductId  = PageHelper.GetProductId();
           
            if (parameters.ProductId == 0)
            {
                // if product id is zero , widget was placed at a non product page so we should show all questions
                questions = _faqRepository.LoadQuestions();
            }
            else
            {
             product = _productRepo.Load(parameters.ProductId);
             questions = _faqRepository.LoadProductQuestions(parameters.ProductId, QuestionType.Answered, ((int)parameters.NoOfFaqs));

            }
            IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();
            AskQuestionModel askQuestionModel = new AskQuestionModel();
            askQuestionModel.ProductId = parameters.ProductId;
            foreach (var faq in questions)
            {
                var model = new QuestionsViewModel();
                model.FAQId = faq.Id;
                model.Question = faq.Question;
                model.Answer = faq.Answer;
                model.ProductName = faq.Product.Name;
                model.Visibility = faq.Visibility;
                model.IsAnswered = faq.IsAnswered;
                model.ProductId = faq.Product.Id;
                questionsList.Add(model);
            }
            ViewBag.ProductId=parameters.ProductId;
            RetailViewModel viewModel = new RetailViewModel();
            viewModel.QuestionsList = questionsList;
            viewModel.AskQuestionModel = askQuestionModel;
            
            return PartialView("~/Plugins/FAQPlugin/Views/_FAQWidget.cshtml", viewModel);
        }
        public ActionResult SubmitQuestion(AskQuestionModel model)
        {
            if (!ModelState.IsValid) {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "Input Field Cannot be Empty" });
            }
            int userId = AbleContext.Current.UserId;
            var createdDate = DateTime.Now;
            var product = _productRepo.Load(model.ProductId);
                var faq = new FAQ()
                {
                    Question = model.Question,
                    Product = product,
                    UserId = userId,
                    CreatedDate = createdDate,
                    IsAnswered = false,
                    Visibility = true,
                };
              _faqRepository.Save(faq);
              TempData["SuccessMessage"] = "Question submitted successfully!";
            
            return Json(new { success = true, message = "Question submitted successfully!" });

          
        }
        
       
    }
}
