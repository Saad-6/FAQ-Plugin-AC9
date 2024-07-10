using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommerceBuilder.Products;
using CommerceBuilder.Web;

namespace FAQPlugin.Models
{
    public class FAQModelParams:WidgetParams
    {
        [DisplayName("Product Id")]
        [Description("Question asked related to this product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [DisplayName("User Id")]
        [Description("User who asked the question")]
        public int UserId { get; set; }

        [DisplayName("Username")]
        [Description("Username of the user who asked the question")]
        public string UserName { get; set; }

        [DisplayName("Login status of user")]
        [Description("Shows different FAQ table depending on if the user is logged in or not")]
        public bool IsUserLoggedIn { get; set; }

        [DisplayName("Question")]
        [Description("The Question asked related to this product")]
        public string Question { get; set; }

        [DisplayName("Answer")]
        [Description("The Answer to the asked question about the product")]
        public string Answer { get; set; }

        [DisplayName("Asked Date")]
        [Description("Question asked at :")]
        public DateTime CreatedDate { get; set; }
    }

   
    
}
