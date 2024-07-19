﻿using AbleCommerce.Code;
namespace FAQPlugin.Controllers
{
    using CommerceBuilder.Common;
    using CommerceBuilder.Products;
    using CommerceBuilder.Web.Mvc;
    using System;
    using System.Web.Mvc;
    using RegisterWidget = CommerceBuilder.Web.RegisterWidget;
    using WidgetCategory = CommerceBuilder.CMS.WidgetCategory;

    public class FAQRetailController : AbleController
    {
        private readonly IProductRepository _productRepo;
        private readonly IFAQRepository _faqRepository;
        private readonly MyNHibernateHelper _NHibernateHelper;
        public FAQRetailController(IProductRepository productRepo, IFAQRepository faqRepository)
        {
            _productRepo = productRepo;
            _faqRepository = faqRepository;
            _NHibernateHelper = new MyNHibernateHelper();
        }

        [RegisterWidget(DisplayName = "Frequently Asked Questions", Category = WidgetCategory.Product, Description = "Displays the most frequently asked questions related with the product.")]
        public ActionResult FAQWidget(Models.FAQModelParams parameters)
        {

            if (parameters.ProductId == 0) { 
            
                parameters.ProductId  = PageHelper.GetProductId();
            }
            var product = _productRepo.Load(parameters.ProductId);
            var questions = _faqRepository.LoadAnsweredProductQuestions(parameters.ProductId);
            parameters.fAQs = questions;
            if (product == null) {
                
            return HttpNotFound();
            
            }

            return PartialView("~/Plugins/FAQPlugin/Views/_FAQWidget.cshtml",parameters);
        }
        public ActionResult SubmitQuestion(string question , int productId)
        {
            int userId = AbleContext.Current.UserId;
           
            var createdDate = DateTime.Now;

            var product = _productRepo.Load(productId);

            if (question == "") {
                Response.StatusCode = 400;
                
                return Json(new { success = false, message = "Input Field Cannot be Empty" });
            }

            if (ModelState.IsValid && productId != 0) {
                var faq = new FAQ()
                {
                    Question = question,
                    Product = product,
                    UserId = userId,
                    CreatedDate = createdDate,
                    IsAnswered = false,
                    Visibility = true,
                };
         

              _faqRepository.Save(faq);

            return Json(new { success = true, message = "Question submitted successfully!" });

            }
            return Json(new { success = false, message = "An Error Occured" });
        }
        
       
    }
}
