using CommerceBuilder.Shipping;
using CommerceBuilder.Web.Mvc;
using FAQPlugin;
using FAQPlugin.Models;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

public class FAQAdminController : AbleAdminController
{
    private readonly IFAQRepository _faqRepository;

    public FAQAdminController(IFAQRepository faqRepository)
    {
        _faqRepository = faqRepository;
    }

    public ActionResult Index()
    {
      
        return View("~/Plugins/FAQPlugin/Views/Index.cshtml");
    }


    public ActionResult Unanswered(int pageNumber = 1,int pageSize = 5,string sortExpression = "Question")
    {
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        int countResult = _faqRepository.GetPendingCount();
        int count = countResult == 0 ? pageSize : countResult;
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        var unAnsweredQuestions = _faqRepository.LoadUnAnsweredQuestions(pageSize,startIndex,sortExpression);
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faq in unAnsweredQuestions) {

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
        StaticPagedList<QuestionsViewModel> pagedList =new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();
        return PartialView("~/Plugins/FAQPlugin/Views/_UnAnsweredFAQs.cshtml", pagedList);
    }


    public ActionResult Answered(int pageNumber = 1, int pageSize = 5, string sortExpression = "Question")
    {
        int answeredCount = _faqRepository.GetAnsweredCount();
        int count = answeredCount == 0 ? pageSize : answeredCount;
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        var answeredQuestions = _faqRepository.LoadAllAnsweredQuestions(pageSize, startIndex, sortExpression);
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faq in answeredQuestions)
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
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();

        return PartialView("~/Plugins/FAQPlugin/Views/_AnsweredFAQs.cshtml", pagedList);
    }


    public ActionResult All(int pageNumber = 1, int pageSize = 5, string sortExpression = "Question")
    {
        int count = _faqRepository.CountAll() == 0 ? pageSize : _faqRepository.CountAll();
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        var allQuestions = _faqRepository.GetAll(pageSize,startIndex, sortExpression);

        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faq in allQuestions)
        {

            var model = new QuestionsViewModel();
            model.FAQId = faq.Id;
            model.Question = faq.Question;
            model.Answer = faq.Answer;
            model.ProductName = faq.Product.Name;
            model.Visibility = faq.Visibility;
            model.ProductId = faq.Product.Id;
            model.IsAnswered = faq.IsAnswered;
            questionsList.Add(model);

        }
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber; ;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();

        return PartialView("~/Plugins/FAQPlugin/Views/_AllFAQs.cshtml", pagedList);
    }

    // This can be called from all three tabs so we need an identifier 'view' in parameter to know where the call was made from and return view accordingly
    public ActionResult UpdateAnswer(int id, string answer, string view, int pageNumber = 1, int pageSize = 5, string sortExpression = "Question")
    {
        var faq = _faqRepository.LoadQuestionById(id);
        faq.Answer = answer;
        faq.IsAnswered = true;
        faq.Visibility = true;
        _faqRepository.Update(faq);

        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);

        StaticPagedList<QuestionsViewModel> pagedList ;
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();
        int count;
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        ViewBag.CurrentPageSize = pageSize;

        // If the request was made from Answered modal it will update the Answered tabs

        if (view == "answered")
        {
             var AnsweredQuestions = _faqRepository.LoadAllAnsweredQuestions(pageSize,startIndex,sortExpression);
             count = _faqRepository.GetAnsweredCount();

            foreach (var faqs in AnsweredQuestions)
            {

                var model = new QuestionsViewModel();
                model.FAQId = faqs.Id;
                model.Question = faqs.Question;
                model.Answer = faqs.Answer;
                model.ProductName = faqs.Product.Name;
                model.Visibility = faqs.Visibility;
                model.IsAnswered = faqs.IsAnswered;
                model.ProductId = faqs.Product.Id;
                questionsList.Add(model);

            }
            pagedList = new StaticPagedList<QuestionsViewModel>(questionsList,pageNumber,pageSize,count);

            return PartialView("~/Plugins/FAQPlugin/Views/_AnsweredFAQs.cshtml", pagedList);

        }

        // If the request was made from ALl Questentions tab , it will update that partial view
        
        else if(view == "all" )
        {
            var allQuestions = _faqRepository.GetAll(pageNumber,pageSize,sortExpression);
            count = _faqRepository.CountAll();

            foreach (var faqs in allQuestions)
            {

                var model = new QuestionsViewModel();
                model.FAQId = faqs.Id;
                model.Question = faqs.Question;
                model.Answer = faqs.Answer;
                model.ProductName = faqs.Product.Name;
                model.Visibility = faqs.Visibility;
                model.IsAnswered = faqs.IsAnswered;
                model.ProductId = faqs.Product.Id;
                questionsList.Add(model);

            }
            pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);

            return PartialView("~/Plugins/FAQPlugin/Views/_AllFAQs.cshtml", pagedList);

        }

       // Otherwise the request was made from un-Answered tab
        
        var unAnsweredQuestions = _faqRepository.LoadUnAnsweredQuestions(pageSize,pageIndex,sortExpression);
        count = _faqRepository.GetPendingCount();
        foreach (var faqs in unAnsweredQuestions)
        {

            var model = new QuestionsViewModel();
            model.FAQId = faqs.Id;
            model.Question = faqs.Question;
            model.Answer = faqs.Answer;
            model.ProductName = faqs.Product.Name;
            model.Visibility = faqs.Visibility;
            model.IsAnswered = faqs.IsAnswered;
            model.ProductId = faqs.Product.Id;
            questionsList.Add(model);

        }
        pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, 1, 5, count);

        return PartialView("~/Plugins/FAQPlugin/Views/_UnAnsweredFAQs.cshtml", pagedList);
    }

    [HttpPost]
    public ActionResult ChangeVisibility(int id, bool visibility)
    {
        var faq = _faqRepository.LoadQuestionById(id);
        if (faq != null)
        {
            faq.Visibility = visibility;
            _faqRepository.Save(faq);
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }
     [HttpPost]
    public ActionResult DeleteFAQ(int id, int pageNumber = 1, int pageSize = 5,string sortExpression = "Question")
    {
        var faq = _faqRepository.LoadQuestionById(id);

        _faqRepository.Remove(faq);
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        var allQuestions = _faqRepository.GetAll(pageSize,pageIndex,sortExpression);
        int count = _faqRepository.CountAll();
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faqs in allQuestions)
        {

            var model = new QuestionsViewModel();
            model.FAQId = faqs.Id;
            model.Question = faqs.Question;
            model.Answer = faqs.Answer;
            model.ProductName = faqs.Product.Name;
            model.Visibility = faqs.Visibility;
            model.IsAnswered = faqs.IsAnswered;
            model.ProductId = faqs.Product.Id;
            questionsList.Add(model);

        }
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList,pageNumber,pageSize, count);
        ViewBag.CurrentPageSize = pageSize;

        return PartialView("~/Plugins/FAQPlugin/Views/_AllFAQs.cshtml", pagedList);
    }


}
