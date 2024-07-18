using CommerceBuilder.DomainModel;
using FAQPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAQPlugin
{
    public interface IFAQRepository : IRepository<FAQ>
    {
        int GetAllCount();
        IList<FAQ> LoadAllProductQuestions(int productId);
        FAQ LoadQuestionById(int id);
        IList<FAQ> LoadAllQuestions();
        IList<FAQ> LoadAllAnsweredQuestions();
        IList<FAQ> LoadUnAnsweredQuestions();
        IList<FAQ> LoadAnsweredProductQuestions(int productId);
        int GetPendingCount();
        int GetAnsweredCount();
        IList<FAQ> GetAll();
        bool Update(FAQ model);
        bool Remove (FAQ model);

    }
}
