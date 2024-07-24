using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class AskQuestionModel
    {
        [Required(ErrorMessage = "Question field cannot be empty !")]
        public string Question { get; set; }
        public int ProductId { get; set; }


    }

}
