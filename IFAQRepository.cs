using CommerceBuilder.DomainModel;
using FAQPlugin.Models;
using System.Collections.Generic;

namespace FAQPlugin
{
    public interface IFAQRepository : IRepository<FAQ>
    {
        int GetCount(QuestionType questionType = QuestionType.All);
        IList<FAQ> LoadProductQuestions(int productId, QuestionType questionType = QuestionType.All, int pageSize = 0);
        IList<FAQ> LoadQuestions(QuestionType questionType = QuestionType.All, int pageSize = 0, int startIndex = 0, SortExpression sortExpression = SortExpression.Question, int productId = 0);

    }
}
