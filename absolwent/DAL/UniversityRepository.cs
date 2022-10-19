using absolwent.Database;
using absolwent.DTO;

namespace absolwent.DAL
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly AbsolwentContext _context;

        public UniversityRepository(AbsolwentContext context)
        {
            _context = context;
        }


        public University? GetUniversity(string login)
        {
            var users = _context.University.Where(user => user.Login == login);
            if (!users.Any())
                return null;
            return users.First();
        }
        public University? GetUniversity(long id)
        {
            var users = _context.University.Where(user => user.Id == id);
            if (!users.Any())
                return null;
            return users.First();
        }

        public void CreateUniversity(University university)
        {
            _context.University.Add(university);
            _context.SaveChanges();
        }
        public string? CreatePasswordResetKey(string login)
        {
            var users = _context.University.Where(user => user.Login == login);
            if (!users.Any())
                return null;
            var user = users.First();
            var date = DateTime.Now;
            var key = BCrypt.Net.BCrypt.HashPassword(user.Login + date);
            user.PasswordResetKey = key;
            user.PasswordResetDate = date;
            _context.SaveChanges();
            return key;
        }

        public void PasswordReset(string password, string key)
        {
            var users = _context.University.Where(user => user.PasswordResetKey == key);
            if (!users.Any())
                throw new Exception("Błędny klucz");
            var user = users.First();

            if (user == null || !BCrypt.Net.BCrypt.Verify(user.Login + user.PasswordResetDate, key))
                throw new Exception("Błędny klucz");

            user.PasswordResetKey = string.Empty;
            user.PasswordResetDate = default(DateTime);
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.SaveChanges();
        }

        public bool ChangeFrequency(long id, int frequency)
        {
            var users = _context.University.Where(user => user.Id == id);
            if (!users.Any())
                return false;
            var user = users.First();
            user.QuestionnaireFrequency = frequency;
            _context.SaveChanges();
            return true;
        }
    }
}
