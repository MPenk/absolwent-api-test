using absolwent.Database;
using absolwent.DTO;

namespace absolwent.DAL
{
    public class GraduateRepository : IGraduateRepository
    {
        private readonly AbsolwentContext _context;

        public GraduateRepository(AbsolwentContext context)
        {
            _context = context;
        }


        public Graduate? GetGraduate(string email)
        {
            var users = _context.Graduate.Where(user => user.Email == email);
            if (!users.Any())
                return null;
            return users.First();
        }
        public List<Graduate> GetGraduates()
        {
            return _context.Graduate.ToList();

        }
        public Graduate? GetGraduate(long id)
        {
            var users = _context.Graduate.Where(user => user.Id == id);
            if (!users.Any())
                return null;
            return users.First();
        }
        public void CreateGraduate(Graduate user)
        {
            if (_context.Graduate.Where(graduate => graduate.Email == user.Email).Any())
                throw new Exception("Taki użytkownik istnieje");
            _context.Graduate.Add(user);
            _context.SaveChanges();
        }

        public void DeleteGraduate(int id)
        {
            var graduate = _context.Graduate.Where(graduate => graduate.Id == id);
            if (!graduate.Any())
                throw new Exception("Taki użytkownik nie istnieje");
            _context.Graduate.Remove(graduate.First());
            _context.SaveChanges();
        }
    }
}
