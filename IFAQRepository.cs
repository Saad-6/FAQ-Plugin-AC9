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
        IList<FAQ> LoadAllQuestions(string sortExpression);
        IList<FAQ> LoadAllQuestions(int pageSize, int startIndex,string sortExpression);
        IList<FAQ> LoadAllAnsweredQuestions(string sortExpression);
        IList<FAQ> LoadAllAnsweredQuestions(int pageSize, int startIndex,string sortExpression);
        IList<FAQ> LoadUnAnsweredQuestions(string sortExpression);
        IList<FAQ> LoadUnAnsweredQuestions(int pageSize,int startIndex, string sortExpression);
        IList<FAQ> LoadAnsweredProductQuestions(int productId);
        int GetPendingCount();
        int GetAnsweredCount();
        IList<FAQ> GetAll(string sortExpression);
        IList<FAQ> GetAll(int pageNumber , int pageSize,string sortExpression );
        bool Update(FAQ model);
        bool Remove (FAQ model);

    }
}
