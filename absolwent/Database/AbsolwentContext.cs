using absolwent.DTO;
using Microsoft.EntityFrameworkCore;

namespace absolwent.Database
{
    public class AbsolwentContext
        : DbContext
    {
        public AbsolwentContext(DbContextOptions<AbsolwentContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Data> Data { get; set; }
        public DbSet<Graduate> Graduate { get; set; }
        public DbSet<Questionnaire> Questionnaire { get; set; }
        public DbSet<University> University { get; set; }

    }
}
