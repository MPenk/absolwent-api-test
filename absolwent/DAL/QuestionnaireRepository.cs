using absolwent.Database;
using absolwent.DTO;
using absolwent.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Graduate = absolwent.DTO.Graduate;

namespace absolwent.DAL
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly IDbContextFactory<AbsolwentContext> _factory;
        private readonly AbsolwentContext _context;
        private readonly IPoolService _poolService;

        public QuestionnaireRepository(IDbContextFactory<AbsolwentContext> factory, AbsolwentContext context, IPoolService poolService)
        {
            _factory = factory;
            _context = context;
            _poolService = poolService;

        }


        public void CreateForYear(int year)
        {

            var _context = (AbsolwentContext)_factory.CreateDbContext();
            var graduates = _context.Graduate.Where(user => user.GraduationYear == year);

            foreach (var graduate in graduates)
            {
                var date = DateTime.Now;

                var questionnaire = new Questionnaire()
                {
                    Graduate = graduate,
                    Filled = false,
                    SendingData = date,
                    Token = BCrypt.Net.BCrypt.HashPassword(graduate.Id.ToString() + date.ToString())
                };
                _context.Questionnaire.Add(questionnaire);
                var message = new MailMessage("noreplay@absolwent.best", graduate.Email, "Nowa ankieta!", $"Witaj, oto link do nowej ankiety: https://test.absolwent.best/survey?key={questionnaire.Token}");
                message.From = new MailAddress("noreplay@absolwent.best", "Ankiety Absolwent.best");
                _poolService.SendMail(message);
            }
            _context.SaveChanges();
        }

        public Graduate CheckToken(string token)
        {
            var questionnaire = _context.Questionnaire.Include("Graduate").Where(item => item.Token == token && item.Filled == false).FirstOrDefault();

            if (questionnaire == null)
                throw new Exception("Ankieta niedostępna");

            return questionnaire.Graduate;
        }
    }
}
