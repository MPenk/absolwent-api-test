using System.ComponentModel.DataAnnotations;

namespace absolwent.DTO
{
    public class Data
    {
        [Key]
        public int Id { get; set; }

        public Questionnaire Questionare { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string EndingDate { get; set; } = string.Empty;
        public string ProffesionalActivity { get; set; } = string.Empty;
        public string Earnings { get; set; } = string.Empty;
        public string CompanySize { get; set; } = string.Empty;
        public string TownSize { get; set; } = string.Empty;
        public string CompanyCategory { get; set; } = string.Empty;
        public string JobSearchTime { get; set; } = string.Empty;
        public string PeriodOfEmployment { get; set; } = string.Empty;
        public string JobSatisfaction { get; set; } = string.Empty;
        public string Training { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

    }
}
