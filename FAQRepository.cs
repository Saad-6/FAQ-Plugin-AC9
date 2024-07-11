using CommerceBuilder.DomainModel;
using FAQPlugin.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace FAQPlugin
{
    [RegisterFor(typeof(IRepository<FAQ>))]
    [RegisterFor(typeof(IFAQRepository))]
    public class FAQRepository : Repository<FAQ>, IFAQRepository
    {
        //public bool Add(FAQModel model)
        //{
        //    if (model == null)
        //    {
        //        return false;
        //    }
            
        //    Save(model);

        //    return true;
        //}
        public IList<FAQ> LoadAllProductQuestions(int productId) {

            var productQuestions = NHibernateHelper.CreateCriteria<FAQ>()
            .Add(Restrictions.Eq("ProductId", productId));

            return productQuestions.List<FAQ>();

        }
        public IList<FAQ> LoadAnsweredProductQuestions(int productId) {

            var productQuestionsAnswered = NHibernateHelper.CreateCriteria<FAQ>()
            .Add(Restrictions.Eq("ProductId", productId))
            .Add(Restrictions.Eq("IsAnswered", true));
             return productQuestionsAnswered.List<FAQ>();

        }


        public IList<FAQ> GetAll()
        {
            var entities = NHibernateHelper.CreateCriteria<FAQ>().List<FAQ>();

            return entities;
            
        }

        public int GetAllCount()
        {
          return CountAll();
         
        }

        public int GetAnsweredCount()
        {
           
          return NHibernateHelper.CreateCriteria<FAQ>()
                .Add(Restrictions.Eq("IsAnswered", true))
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();
        }

        public int GetPendingCount()
        {
          return NHibernateHelper.CreateCriteria<FAQ>()
                .Add(Restrictions.Eq("IsAnswered", false))
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();
        }

        public bool Remove(FAQ model)
        {
            if(model == null)
            {
                return false;
            }
            Delete(model);

            return true;
            
        }

        public bool Update(FAQ model)
        {
            return false;
        }
    }
}
