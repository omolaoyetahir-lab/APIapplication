using Microsoft.EntityFrameworkCore;

namespace StudentAPI
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
    }
}
