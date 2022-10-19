using System.ComponentModel.DataAnnotations;

namespace absolwent.DTO
{
    public class University
    {
        [Key]
        public long Id { get; set; }

        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int QuestionnaireFrequency { get; set; }

        public string PasswordResetKey { get; set; } = string.Empty;
        public DateTime PasswordResetDate { get; set; }


    }
}
