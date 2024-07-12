using System.Web.Mvc;
namespace FAQPlugin.Controllers
{
    using CommerceBuilder.Web.Mvc;
    using System.Linq;
    using System.Web.UI;

    public class FAQAdminController : AbleAdminController
    {
        public ActionResult Index() { 
        
        
            return View("~/Plugins/FAQPlugin/Views/Index.cshtml");
        }
      

    }
}
