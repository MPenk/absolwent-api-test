using absolwent.DTO;

namespace absolwent.DAL
{
    public interface IUniversityRepository
    {
        bool ChangeFrequency(long id, int frequency);
        string? CreatePasswordResetKey(string login);
        void CreateUniversity(University university);
        University? GetUniversity(string login);
        University? GetUniversity(long id);
        void PasswordReset(string password, string key);
    }
}