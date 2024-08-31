using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin.Models
{
    public class RetailViewModel
    {
       public StaticPagedList<QuestionsViewModel> PagedList { get; set; }
       public AskQuestionModel AskQuestionModel { get; set; }
        public FAQSettings CurrentSettings { get; set; }
        public RetailViewModel()
        {
            AskQuestionModel = new AskQuestionModel();
            CurrentSettings = new FAQSettings();
        }
    }
}
