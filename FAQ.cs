using CommerceBuilder.DomainModel;
using CommerceBuilder.Products;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAQPlugin
{
    public class FAQ : Entity
    {
        public override int Id { get; set; }
        [Required]
        public virtual Product Product { get; set; }
        public virtual int UserId { get; set; }
        [Required]
        public virtual string Question { get; set; }
        public virtual bool IsAnswered { get; set; } 
        public virtual bool Visibility { get; set; }
        public virtual string Answer { get; set; }
        public virtual DateTime CreatedDate { get; set; }
       
    }
  

}
