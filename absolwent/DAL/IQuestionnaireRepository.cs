using absolwent.DTO;

namespace absolwent.DAL
{
    public interface IQuestionnaireRepository
    {
        Graduate CheckToken(string token);
        void CreateForYear(int year);
    }
}