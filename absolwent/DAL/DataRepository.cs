using absolwent.Database;
using absolwent.DTO;
using absolwent.Models;

namespace absolwent.DAL
{
    public class DataRepository : IDataRepository
    {
        private readonly AbsolwentContext _context;

        public DataRepository(AbsolwentContext context)
        {
            _context = context;
        }

        public Statistics GetStatistics(string name, string label, int year = default, string? gender = null)
        {
            var filter = _context.Data.AsQueryable();
            if (year != 0)
                filter = filter.Where(item => item.EndingDate == year.ToString());
            if (gender != null)
                filter = filter.Where(item => item.Gender == gender.ToString());
            List<Answer> results = new List<Answer>();
            switch (name)
            {
                case "earnings":
                    results = filter.GroupBy(item => item.Earnings).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "companySize":
                    results = filter.GroupBy(item => item.CompanySize).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "townSize":
                    results = filter.GroupBy(item => item.TownSize).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "companyCategory":
                    results = filter.GroupBy(item => item.CompanyCategory).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "location":
                    results = filter.GroupBy(item => item.Location).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "training":
                    results = filter.GroupBy(item => item.Training).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "jobSatisfaction":
                    results = filter.GroupBy(item => item.JobSatisfaction).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "periodOfEmployment":
                    results = filter.GroupBy(item => item.PeriodOfEmployment).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                case "jobSearchTime":
                    results = filter.GroupBy(item => item.JobSearchTime).Select(g => new Answer
                    {
                        Count = g.Count(),
                        Name = g.Key
                    }).ToList();
                    break;
                default:
                    break;
            }

            var response = new Statistics
            {
                Name = label,
                Answers = new List<Answer>()

            };
            for (int i = 1; i <= results.Count; i++)
            {
                results[i - 1].Id = i;
                response.Answers.Add(results[i - 1]);
            }
            return response;
        }

        public void Create(SurveyCreate request)
        {
            var date = DateTime.Now;

            var questionnaire = _context.Questionnaire.Where(quest => quest.Token == request.Token && quest.Filled == false).FirstOrDefault();
            if (questionnaire == null)
                throw new Exception("Ankieta niedostępna");

            var data = new Data
            {
                ProffesionalActivity = request.ProffesionalActivity,
                CompanyCategory = request.CompanyCategory,
                CompanySize = request.CompanySize,
                Earnings = request.Earnings,
                Gender = request.Gender,
                JobSearchTime = request.JobSearchTime,
                PeriodOfEmployment = request.PeriodOfEmployment,
                TownSize = request.TownSize,
                EndingDate = request.EndingDate,
                JobSatisfaction = request.JobSatisfaction,
                Location = request.Location,
                Training = request.Training

            };
            data.Questionare = questionnaire;
            _context.Add(data);
            questionnaire.Filled = true;
            questionnaire.FillingData = date;

            _context.SaveChanges();
        }

        List<string> earnings = new List<string> { "3010-4400", "4401-5900", "5901-7400", "7401-8900", "8901-10400", "10401-11900", "11901-14900", "14900+" };
        List<string> companySize = new List<string> { "Mikroprzedsiębiorstwo (mniej niż 10 pracowników)", "Małe przedsiębiorstwo (mniej niż 50 pracowników)", "Średnie przedsiębiorstwo (mniej niż 250 pracowników)", "Duże przedsiębiorstwo (więcej niż 250 pracowników)" };
        List<string> townSize = new List<string> { "Poniżej 10 000 mieszkańców", "Od 10 000 do 50 000 mieszkańców", "Od 50 001 do 100 000 mieszkańców", "100 001 do 250 000 mieszkańców", "Powyżej 250 000 mieszkańców" };

        List<string> yesNo = new List<string> { "Tak", "Nie" };
        List<string> yesNo2 = new List<string> { "Tak", "Nie", "Trudno stwierdzić" };
        List<string> CompanyCategory = new List<string> { "IT", "Mechaniczna", "Nawigacyjna", "Ekonomiczna", "Elektroniczna/Elektryczna", "Gastronomia", "Dietetyka", "Marketing", "Edukacja", "inne" };
        List<string> jobSearchTime = new List<string> { @"Mniej niż 6 miesięcy", "6 -12 miesięcy", "13-18 miesięcy", "19-24 miesięcy", "Powyżej 24 miesięcy" };
        List<string> periodOfEmployment = new List<string> { @"Mniej niż 1 rok", "1-3 lata", "3-5 lat", "5-10 lat", "Powyżej 10 lat" };


        List<string> Gender = new List<string> { "Mężczyzna", "Kobieta" };

        public void CreateRandom(int count)
        {
            var random = new Random();
            var date = DateTime.Now;
            var questionnaires = _context.Questionnaire.ToList();

            int index = random.Next(questionnaires.Count);
            for (int i = 0; i < count; i++)
            {
                var data = new Data
                {
                    ProffesionalActivity = yesNo[random.Next(yesNo.Count)],
                    CompanyCategory = CompanyCategory[random.Next(CompanyCategory.Count)],
                    CompanySize = companySize[random.Next(companySize.Count)],
                    Earnings = earnings[random.Next(earnings.Count)],
                    Gender = Gender[random.Next(Gender.Count)],
                    JobSearchTime = jobSearchTime[random.Next(jobSearchTime.Count)],
                    PeriodOfEmployment = periodOfEmployment[random.Next(periodOfEmployment.Count)],
                    TownSize = townSize[random.Next(townSize.Count)],
                    EndingDate = random.Next(1990, 2021).ToString(),
                    JobSatisfaction = yesNo2[random.Next(yesNo.Count)],
                    Training = yesNo[random.Next(yesNo.Count)],
                    Location = yesNo[random.Next(yesNo.Count)],
                };
                data.Questionare = questionnaires[random.Next(questionnaires.Count)];
                _context.Add(data);
                data.Questionare.Filled = true;
                data.Questionare.FillingData = date;
            }



            _context.SaveChanges();
        }


    }
}
