using CommerceBuilder.Web.Mvc;
using FAQPlugin;
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


    public ActionResult Unanswered()
    {
        var unAnsweredQuestions = _faqRepository.LoadUnAnsweredQuestions(); 


        
        return PartialView("~/Plugins/FAQPlugin/Views/_UnAnsweredFAQs.cshtml", unAnsweredQuestions);
    }


    public ActionResult Answered()
    {
        var answeredQuestions = _faqRepository.LoadAllAnsweredQuestions(); 
        return PartialView("~/Plugins/FAQPlugin/Views/_AnsweredFAQs.cshtml", answeredQuestions);
    }


    public ActionResult All()
    {
        var allQuestions = _faqRepository.GetAll();
        return PartialView("~/Plugins/FAQPlugin/Views/_AllFAQs.cshtml", allQuestions);
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
