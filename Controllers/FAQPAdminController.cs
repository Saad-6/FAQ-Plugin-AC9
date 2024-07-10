using System.Web.Mvc;
using System;
using System.Web;
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
        
        
            return PartialView("~/Plugins/FAQPlugin/Views/Index.cshtml");
        }

    }
}
