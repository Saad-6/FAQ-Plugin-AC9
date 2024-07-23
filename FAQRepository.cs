using CommerceBuilder.DomainModel;
using FAQPlugin.Models;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace FAQPlugin
{
    [RegisterFor(typeof(IRepository<FAQ>))]
    [RegisterFor(typeof(IFAQRepository))]
    public class FAQRepository : Repository<FAQ>, IFAQRepository
    {
        public IList<FAQ> LoadProductQuestions(int productId, QuestionType questionType = QuestionType.All, int pageSize = 0)
        {
            SortExpression sortExpression = SortExpression.Question;
            return LoadQuestions(questionType, pageSize, 0, sortExpression, productId);
        }
        public IList<FAQ> LoadQuestions(QuestionType questionType = QuestionType.All, int pageSize = 0, int startIndex = 0, SortExpression sortExpression = SortExpression.Question, int productId = 0)
        {
            var questions = NHibernateHelper.CreateCriteria<FAQ>().AddOrder(Order.Asc(sortExpression.ToString()));

            if (productId != 0)
            {
                questions.
                CreateAlias("Product", "product")
               .Add(Restrictions.Eq("product.Id", productId))
               .Add(Restrictions.Eq("Visibility", true));
            }

            if (questionType == QuestionType.Unanswered)
            {
                questions.Add(Restrictions.Eq("IsAnswered", false));
            } 
            else if (questionType == QuestionType.Answered)
            {
                questions.Add(Restrictions.Eq("IsAnswered", true));
            }

            if (pageSize != 0)
            {
                questions
               .SetMaxResults(pageSize);
            }
            if (startIndex != 0)
            {
                questions
               .SetFirstResult(startIndex);
            }
            return questions.List<FAQ>();
        }
        public int GetCount(QuestionType questionType = QuestionType.All)
        {
            var count = NHibernateHelper.CreateCriteria<FAQ>().SetProjection(Projections.RowCount());
     
            if (questionType == QuestionType.Answered)
            {
                 count
                .Add(Restrictions.Eq("IsAnswered", true));
            }
            if (questionType == QuestionType.Unanswered)
            {
                count
               .Add(Restrictions.Eq("IsAnswered", false));
            }
            return count
                  .UniqueResult<int>();
        }
    }
}
