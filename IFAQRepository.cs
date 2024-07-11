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
        IList<FAQ> LoadAnsweredProductQuestions(int productId);
        int GetPendingCount();
        int GetAnsweredCount();
        IList<FAQ> GetAll();
        //bool Add(FAQModel model);
        bool Update(FAQ model);
        bool Remove (FAQ model);

    }
}
