namespace absolwent.Models
{
    public class GraduatesListResponse : Response
    {
        public List<absolwent.DTO.Graduate> Graduates { get; set; } = new List<DTO.Graduate>();
    }
}
