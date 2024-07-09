using System.Web.Mvc;
namespace FAQPlugin.Controllers
{
    using System.Linq;
    using CommerceBuilder.Common;
    using CommerceBuilder.Utility;
    using CommerceBuilder.Web.Mvc;
    using Models;
    
    public class FAQAdminController : AbleAdminController
    {
        public ActionResult Index() { 
        
        
            return View("~/Plugins/FAQPlugin/Views/Index.cshtml");
        }

    }
}
