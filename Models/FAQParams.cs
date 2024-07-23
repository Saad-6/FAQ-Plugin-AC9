using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AbleCommerce.Models;
using CommerceBuilder.Products;
using CommerceBuilder.Web;

namespace FAQPlugin.Models
{
    public class FAQParams : WidgetParams
    {
        [Browsable(true), DefaultValue(FAQDisplayNumber.Five)]
        [Description("The number of FAQs to display.")]
        public FAQDisplayNumber NoOfFaqs { get; set; }

        [Browsable(false)]
        public int ProductId { get; set; }

        public FAQParams() { 
        
            NoOfFaqs = FAQDisplayNumber.Five;
        }

    }
    public enum FAQDisplayNumber
    {
        Three = 3,
        Five = 5,
        Ten = 10,
        Fifteen = 15,
        Twenty = 20
    }



}
