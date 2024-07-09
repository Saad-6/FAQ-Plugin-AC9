using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommerceBuilder.Web;

namespace FAQPlugin.Models
{
    public class FAQModel
    {
        public int Id { get; set; }

        public int CreatedById { get; set; }

        public string CreatedByName { get; set; }
        
        [Required]
        public string Author { get; set; }

        [Required]
        public string Question{ get; set; }

        [Required]
        public string Answer{ get; set; }

        public DateTime CreatedDate { get; set; }
    }

   
    
}
