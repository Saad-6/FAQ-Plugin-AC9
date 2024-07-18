using CommerceBuilder.DomainModel;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAQPlugin
{
    public class FAQ : Entity
    {
                    //Question = question,
                    //ProductId = productId,
                    //UserId = userId,
                    //CreatedDate = createdDate,
                    //IsAnswered = false,
                    //Visibility = true,
                    //ProductName=product.Name
        public override int Id { get; set; }
        [Required]
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int UserId { get; set; }
        [Required]
        public virtual string Question { get; set; }
        public virtual bool IsAnswered { get; set; } 
        public virtual bool Visibility { get; set; }
        public virtual string Answer { get; set; }
        public virtual DateTime CreatedDate { get; set; }
       
    }
  

}
