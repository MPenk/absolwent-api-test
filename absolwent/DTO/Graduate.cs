using System.ComponentModel.DataAnnotations;

namespace absolwent.DTO
{
    public class Graduate
    {

        [Key]
        public long Graduate_id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Graduation_year { get; set; }
        public string Faculty { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
