using CommerceBuilder.CMS;
using AbleCommerce.Code;
using AbleCommerce;
namespace FAQPlugin.Controllers
{
    using CommerceBuilder.Web.Mvc;
    using System.Web.Mvc;
    using WidgetCategory = CommerceBuilder.CMS.WidgetCategory;
    using RegisterWidget = CommerceBuilder.Web.RegisterWidget;
    using global::FAQPlugin.Models;
    using CommerceBuilder.Products;
    using NHibernate.Hql.Ast.ANTLR.Util;
    using CommerceBuilder.Users;

    public class FAQRetailController : AbleController
    {
        private readonly IProductRepository _productRepo;

        public FAQRetailController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public ActionResult Index() {


            return View("~/Plugins/FAQPlugin/Views/Index.cshtml");
        }
        [RegisterWidget(DisplayName = "Frequently Asked Questions", Category = WidgetCategory.Product, Description = "Displays the most frequently asked questions related with the product.")]
        public ActionResult FAQWidget(FAQModelParams parameters)
        {
            if (parameters.ProductId == 0 || parameters.ProductId == null) { 
            
                parameters.ProductId  = PageHelper.GetProductId();
            }
            var product = _productRepo.Load(parameters.ProductId);
            parameters.Answer = User.Identity.Name;
            if (product == null) {
                
            return HttpNotFound();
            
            }
            parameters.Product = product;

            return PartialView("~/Plugins/FAQPlugin/Views/_FAQWidget.cshtml",parameters);
        }
    }
}
