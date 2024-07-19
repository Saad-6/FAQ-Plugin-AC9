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
        IList<FAQ> LoadAllProductQuestions(int productId);
        FAQ LoadQuestionById(int id);
        IList<FAQ> LoadAllQuestions();
        IList<FAQ> LoadAllQuestions(int pageSize, int startIndex);
        IList<FAQ> LoadAllAnsweredQuestions();
        IList<FAQ> LoadAllAnsweredQuestions(int pageSize, int startIndex);
        IList<FAQ> LoadUnAnsweredQuestions();
        IList<FAQ> LoadUnAnsweredQuestions(int pageSize,int startIndex);
        IList<FAQ> LoadAnsweredProductQuestions(int productId);
        int GetPendingCount();
        int GetAnsweredCount();
        IList<FAQ> GetAll();
        IList<FAQ> GetAll(int pageNumber , int pageSize );
        bool Update(FAQ model);
        bool Remove (FAQ model);

    }
}
