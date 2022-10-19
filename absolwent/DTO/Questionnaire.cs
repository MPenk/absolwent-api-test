using System.ComponentModel.DataAnnotations;
namespace absolwent.DTO
{
    public class Questionnaire
    {
        [Key]
        public long Id { get; set; }
        public Graduate Graduate { get; set; }
        public DateTime FillingData { get; set; }
        public DateTime SendingData { get; set; }
        public string Token { get; set; }
        public bool Filled { get; set; }

    }
}