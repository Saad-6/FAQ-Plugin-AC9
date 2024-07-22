namespace FAQPlugin.Models
{
    public class QuestionsViewModel
    {
      public int FAQId { get; set; }
      public string ProductName { get; set; }
      public int ProductId { get; set; }
      public string Question { get; set; }
      public string Answer { get; set; }
      public bool IsAnswered { get; set; }
      public bool Visibility { get; set; }  

    }
}
