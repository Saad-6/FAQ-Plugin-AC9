using CommerceBuilder.DomainModel;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace FAQPlugin
{
    [RegisterFor(typeof(IRepository<FAQ>))]
    [RegisterFor(typeof(IFAQRepository))]
    public class FAQRepository : Repository<FAQ>, IFAQRepository
    {
        public IList<FAQ> LoadAllProductQuestions(int productId) {

            var productQuestions = NHibernateHelper.CreateCriteria<FAQ>()
            .Add(Restrictions.Eq("ProductId", productId))
            .Add(Restrictions.Eq("Visibility", true));


            return productQuestions.List<FAQ>();

        }
        public FAQ LoadQuestionById(int id)
        {
            var criteria = NHibernateHelper.CreateCriteria<FAQ>()
            .Add(Restrictions.Eq("Id", id));

            return criteria.UniqueResult<FAQ>();
        }
        public IList<FAQ> LoadAnsweredProductQuestions(int productId)
        {
                 var loadAnsweredProductQuestions = NHibernateHelper.CreateCriteria<FAQ>()
                .CreateAlias("Product", "product") 
                .Add(Restrictions.Eq("product.Id", productId)) 
                .Add(Restrictions.Eq("IsAnswered", true))
                .Add(Restrictions.Eq("Visibility", true));
                
            return loadAnsweredProductQuestions.List<FAQ>();
        }
        public IList<FAQ> LoadAllQuestions()
        {
            var loadAllQuestions = NHibernateHelper.CreateCriteria<FAQ>();
           
            return loadAllQuestions.List<FAQ>();    
        } 
        public IList<FAQ> LoadAllQuestions(int pageSize, int startIndex)
        {

            var loadAllQuestions = NHibernateHelper.CreateCriteria<FAQ>()
           .SetFirstResult(startIndex)
           .SetMaxResults(pageSize);

            return loadAllQuestions.List<FAQ>();    
        }
        public IList<FAQ> LoadAllAnsweredQuestions()
        {
            var loadAllAnsweredQuestions = NHibernateHelper.CreateCriteria<FAQ>()
           .Add(Restrictions.Eq("IsAnswered", true));

            return loadAllAnsweredQuestions.List<FAQ>();

        }
        public IList<FAQ> LoadAllAnsweredQuestions(int pageSize, int startIndex)
        {

            var loadAllAnsweredQuestions = NHibernateHelper.CreateCriteria<FAQ>()
           .Add(Restrictions.Eq("IsAnswered", true))
           .SetFirstResult(startIndex)
           .SetMaxResults(pageSize);

            return loadAllAnsweredQuestions.List<FAQ>();

        }

        public IList<FAQ> LoadUnAnsweredQuestions()
        {
            var loadAllUnansweredQuestions = NHibernateHelper.CreateCriteria<FAQ>()
          .Add(Restrictions.Eq("IsAnswered", false));

            return loadAllUnansweredQuestions.List<FAQ>();

        } 
        public IList<FAQ> LoadUnAnsweredQuestions(int pageSize,int startIndex)
        {
            var loadAllUnansweredQuestions = NHibernateHelper.CreateCriteria<FAQ>()
           .Add(Restrictions.Eq("IsAnswered", false))
           .SetFirstResult(startIndex) 
           .SetMaxResults(pageSize); 

            return loadAllUnansweredQuestions.List<FAQ>();

        }
        public IList<FAQ> GetAll()
        {
            var entities = NHibernateHelper.CreateCriteria<FAQ>().List<FAQ>();

            return entities;
            
        }
        public IList<FAQ> GetAll(int pageSize , int startIndex )
        {
            var entities = NHibernateHelper.CreateCriteria<FAQ>()
            .SetFirstResult(startIndex)
            .SetMaxResults(pageSize).List<FAQ>();
               
            return entities;
            
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
            if(model == null)
            {
                return false;
            }

            Save(model);
            return true;
        }
    }
}
