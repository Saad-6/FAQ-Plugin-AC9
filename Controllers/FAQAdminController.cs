using CommerceBuilder.Common;
using CommerceBuilder.Web.Mvc;
using FAQPlugin;
using FAQPlugin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FAQPlugin.Models;
using FAQPlugin.Utility;
public class FAQAdminController : AbleAdminController
{
    private readonly IFAQRepository _faqRepository;
    private const string AnsweredView = "_AnsweredFAQs";
    private const string UnnsweredView = "_UnAnsweredFAQs";
    private const string AllView = "_AllFAQs";
    public FAQAdminController(IFAQRepository faqRepository)
    {
        _faqRepository = faqRepository;
    }
    public ActionResult Index()
    {
        return View("~/Plugins/FAQPlugin/Views/Index.cshtml");
    }
    public ActionResult Configure()
    {
      var currentSettings = AbleContext.Current.Store.Settings;
        var viewModel = new FAQSettings()
        {
            AllowAnonymousUsers = currentSettings.GetValueByKey("FAQ_AllowAnonymousUsers") == "True",
            DefaultResponderName = currentSettings.GetValueByKey("FAQ_DefaultResponderName")
        };
        return View("~/Plugins/FAQPlugin/Views/Configure.cshtml",viewModel);
    }
    [HttpPost]
    public ActionResult SaveSettings(FAQSettings model)
    {
        var currentSettings = AbleContext.Current.Store.Settings;
        currentSettings.SetValueByKey("FAQ_AllowAnonymousUsers", model.AllowAnonymousUsers.ToString());
        currentSettings.SetValueByKey("FAQ_DefaultResponderName", model.DefaultResponderName);
        var viewModel = new FAQSettings()
        {
            AllowAnonymousUsers = currentSettings.GetValueByKey("FAQ_AllowAnonymousUsers") == "True",
            DefaultResponderName = currentSettings.GetValueByKey("FAQ_DefaultResponderName")
        };
        currentSettings.Save();
        ViewBag.Success = true;
        return View("~/Plugins/FAQPlugin/Views/Configure.cshtml", viewModel); 
    }
    public ActionResult DisplayTabs(string view, int pageNumber = 1, int pageSize = 5, SortExpression sortExpression = SortExpression.Question)
    {
        int count;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        QuestionType questionType = QuestionType.All;
        IList<FAQ> questions = new List<FAQ>();
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        if (view == AnsweredView)
        {
            questionType = QuestionType.Answered;
        }
        else if (view == UnnsweredView)
        {
            questionType = QuestionType.Unanswered;
        }
        else
        {
            questionType = QuestionType.All;
        }
        questions = _faqRepository.LoadQuestions(questionType, pageSize, startIndex, sortExpression);
        count = _faqRepository.GetCount(questionType);

        foreach (var faq in questions)
        {
            try
            {
                var model = new QuestionsViewModel
                {
                    FAQId = faq.Id,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    ProductName = faq.Product.Name, // This line can throw ObjectNotFoundException
                    Visibility = faq.Visibility,
                    IsAnswered = faq.IsAnswered,
                    ProductId = faq.Product.Id,
                    LastModified = faq.LastModified
                    
                };
                questionsList.Add(model);
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                _faqRepository.Delete(faq);
            }
        }
        // Check if there are no questions
        if (count == 0)
        {
            pageSize = 1;
            pageNumber = 1;
            StaticPagedList<QuestionsViewModel> emptyPagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;
            ViewBag.CurrentPageSize = "Show All";
            return PartialView($"~/Plugins/FAQPlugin/Views/{view}.cshtml", emptyPagedList);
        }
        // If the page size was selected to be greater than the number of faqs or if page size is -1 (value for show all) , pageSize = count
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();
        return PartialView($"~/Plugins/FAQPlugin/Views/{view}.cshtml", pagedList);
    }
    // This action is for both Give Answer and Update Answer
    public ActionResult UpdateAnswer(int id, string answer, string view, int pageNumber = 1, int pageSize = 5, string sortExpression = "Question")
    {
        var faq = _faqRepository.Load(id);
        if(faq == null)
        {
            return Index();
        }
        var settings = AbleContext.Current.Store.Settings;
        faq.Answer = answer;
        faq.IsAnswered = true;
        faq.LastModified = DateTime.Now;
        faq.AnswerBy = settings.GetValueByKey("FAQ_DefaultResponderName");
        _faqRepository.Save(faq);

        return DisplayTabs(view, pageNumber, pageSize);
    }

    [HttpPost]
    public ActionResult ChangeVisibility(int id, bool visibility,string view,int pageNumber,int pageSize)
    {
        var faq = _faqRepository.Load(id);
        if (faq == null)
        {
            return Index();
        }
        faq.Visibility = visibility;
        _faqRepository.Save(faq);
        return DisplayTabs(view, pageNumber, pageSize);
    }
     [HttpPost]
    public ActionResult DeleteFAQ(int id, string view,int pageNumber = 1, int pageSize = 5,string sortExpression = "Question")
    {
        var faq = _faqRepository.Load(id);
        if (faq == null)
        {
            return Index();
        }
        _faqRepository.Delete(faq);
        return DisplayTabs(view,pageNumber, pageSize);
    }
}
