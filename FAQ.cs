using CommerceBuilder.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class FAQ : Entity
    {
        public int Id { get; set; }
        public bool IsAnswered { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
