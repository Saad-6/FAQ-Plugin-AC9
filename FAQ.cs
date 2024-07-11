using CommerceBuilder.DomainModel;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.ComponentModel.DataAnnotations;

namespace FAQPlugin
{
    public class FAQ : Entity
    {
        public override int Id { get; set; }
        [Required]
        public virtual int ProductId { get; set; }
        public virtual int UserId { get; set; }
        [Required]
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
        public virtual DateTime CreatedDate { get; set; }
       
    }
    public class FAQMap : ClassMapping<FAQ>
    {
        public FAQMap()
        {
            Table("FAQ");
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.ProductId, map => map.NotNullable(true));
            Property(x => x.UserId);
            Property(x => x.Question, map => map.NotNullable(true));
            Property(x => x.Answer);
            Property(x => x.CreatedDate);
        }
    }

}
