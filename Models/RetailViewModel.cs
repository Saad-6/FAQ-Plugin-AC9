using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class RetailViewModel
    {
       public IList<QuestionsViewModel> QuestionsList { get; set; }

       public AskQuestionModel AskQuestionModel { get; set; }

        public RetailViewModel()
        {
            QuestionsList = new List<QuestionsViewModel>();
            AskQuestionModel = new AskQuestionModel();
        }
 
          

    }
}
