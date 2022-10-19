using System.ComponentModel.DataAnnotations;

namespace absolwent.DTO
{
    public class Graduate
    {

        [Key]
        public long Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int GraduationYear { get; set; }
        public string Faculty { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
