using absolwent.Models;

namespace absolwent.DAL
{
    public interface IDataRepository
    {
        void Create(SurveyCreate request);
        void CreateRandom(int count);
        Statistics GetStatistics(string name, string label, int year = 0, string? gender = null);
    }
}