using CommerceBuilder.Web.Mvc;
using FAQPlugin;
using FAQPlugin.Models;
using PagedList;
using System.Collections.Generic;
using System.Web.Mvc;

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
    public ActionResult DisplayTabs(string view, int pageNumber = 1, int pageSize = 5, SortExpression sortExpression = SortExpression.Question )
    {
        int count;
        int pageIndex = pageNumber - 1;
        int startIndex = (pageSize * pageIndex);
        QuestionType questionType = QuestionType.All;
        IList<FAQ>questions = new List<FAQ>();
        IList<QuestionsViewModel> questionsList = new List<QuestionsViewModel>();
        if (view == AnsweredView)
        {
            questionType = QuestionType.Answered;
        }
        else if(view == UnnsweredView)
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
      
        pageSize = pageSize == -1 || pageSize > count ? count : pageSize;
        StaticPagedList<QuestionsViewModel> pagedList = new StaticPagedList<QuestionsViewModel>(questionsList, pageNumber, pageSize, count);
        ViewBag.PageSize = pageSize;
        ViewBag.PageNumber = pageNumber;
        ViewBag.CurrentPageSize = pageSize == -1 ? "Show All" : pageSize.ToString();
        return PartialView($"~/Plugins/FAQPlugin/Views/{view}.cshtml", pagedList);
    }


    public ActionResult UpdateAnswer(int id, string answer, string view, int pageNumber = 1, int pageSize = 5, string sortExpression = "Question")
    {
        var faq = _faqRepository.Load(id);
        faq.Answer = answer;
        faq.IsAnswered = true;
        _faqRepository.Save(faq);

        return DisplayTabs(view, pageNumber, pageSize);
    }

    [HttpPost]
    public ActionResult ChangeVisibility(int id, bool visibility)
    {
        var faq = _faqRepository.Load(id);
        if (faq != null)
        {
             faq.Visibility = visibility;
            _faqRepository.Save(faq);
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }
     [HttpPost]
    public ActionResult DeleteFAQ(int id, string view,int pageNumber = 1, int pageSize = 5,string sortExpression = "Question")
    {
        var faq = _faqRepository.Load(id);
        _faqRepository.Delete(faq);
        return DisplayTabs(view,pageNumber, pageSize);
    }
}
