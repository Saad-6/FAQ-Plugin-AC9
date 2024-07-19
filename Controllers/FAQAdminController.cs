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


    public ActionResult Unanswered(int pageNumber = 1,int pageSize = 5)
    {
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        int count = _faqRepository.GetPendingCount() == 0 ? pageSize : _faqRepository.GetPendingCount();
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        var unAnsweredQuestions = _faqRepository.LoadUnAnsweredQuestions(pageSize,startIndex);
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faq in unAnsweredQuestions) {

            var model = new QuestionsViewModel();
            model.FAQId = faq.Id;
            model.Question = faq.Question;
            model.Answer = faq.Answer;
            model.ProductName = faq.Product.Name;
            model.Visibility = faq.Visibility;
            model.IsAnswered = faq.IsAnswered;
            questionsList.Add(model);
        
        }
        StaticPagedList<QuestionsViewModel> pagedList =new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.pageSize = pageSize;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();
        return PartialView("~/Plugins/FAQPlugin/Views/_UnAnsweredFAQs.cshtml", pagedList);
    }


    public ActionResult Answered(int pageNumber = 1, int pageSize = 5)
    {
        int count = _faqRepository.GetAnsweredCount() == 0 ? pageSize : _faqRepository.GetAnsweredCount();
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        var answeredQuestions = _faqRepository.LoadAllAnsweredQuestions(pageSize, startIndex);
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
            questionsList.Add(model);

        }
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.pageSize = pageSize;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();

        return PartialView("~/Plugins/FAQPlugin/Views/_AnsweredFAQs.cshtml", pagedList);
    }


    public ActionResult All(int pageNumber = 1, int pageSize = 5)
    {
        int count = _faqRepository.CountAll() == 0 ? pageSize : _faqRepository.CountAll();
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        var allQuestions = _faqRepository.GetAll(pageSize,startIndex);

        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();

        foreach (var faq in allQuestions)
        {

            var model = new QuestionsViewModel();
            model.FAQId = faq.Id;
            model.Question = faq.Question;
            model.Answer = faq.Answer;
            model.ProductName = faq.Product.Name;
            model.Visibility = faq.Visibility;
            model.IsAnswered = faq.IsAnswered;
            questionsList.Add(model);

        }
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.pageSize = pageSize;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();

        return PartialView("~/Plugins/FAQPlugin/Views/_AllFAQs.cshtml", pagedList);
    }
    public ActionResult UpdateAnswer(int id, string answer)
    {
        var faq = _faqRepository.LoadQuestionById(id);
        
        faq.Answer = answer;

        faq.IsAnswered = true;
        faq.Visibility = true;
        
        _faqRepository.Update(faq);

        return RedirectToAction(nameof(Index));
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
    public ActionResult DeleteFAQ(int id)
    {
        var faq = _faqRepository.LoadQuestionById(id);

        _faqRepository.Remove(faq);

        return  RedirectToAction(nameof(Index));
    }


}
