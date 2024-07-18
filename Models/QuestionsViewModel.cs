using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class QuestionsViewModel
    {
        public IList<FAQ> AllAnsweredQuestions { get; set; }
        public IList<FAQ> AllUnAnsweredQuestions { get; set; }
        public IList<FAQ> AllQuestions { get; set; }

    }
}
