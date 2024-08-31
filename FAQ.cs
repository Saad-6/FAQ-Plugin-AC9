using CommerceBuilder.DomainModel;
using CommerceBuilder.Products;
using CommerceBuilder.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAQPlugin
{
    public class FAQ : Entity
    {
        public override int Id { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        public virtual User User { get; set; }

        [Required]
        public virtual string Question { get; set; }

        public virtual bool IsAnswered { get; set; }
        public virtual bool Visibility { get; set; }
        public virtual string Answer { get; set; }
        public virtual string AnswerBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime LastModified { get; set; }
    }
}
