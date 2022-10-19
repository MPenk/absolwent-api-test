using absolwent.DTO;

namespace absolwent.DAL
{
    public interface IGraduateRepository
    {
        void CreateGraduate(Graduate user);
        void DeleteGraduate(int id);
        Graduate? GetGraduate(long id);
        Graduate? GetGraduate(string email);
        List<Graduate> GetGraduates();
    }
}