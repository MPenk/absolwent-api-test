namespace absolwent.Models
{
    public class Statistics
    {

        public long AnswersCount { get; set; }
        public List<Answer> Answers { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
