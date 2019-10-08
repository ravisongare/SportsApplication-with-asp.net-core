using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsApplication.Models
{
    public class SportsApplicationDbContext:IdentityDbContext
    {
        public SportsApplicationDbContext(DbContextOptions<SportsApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Athlete> Athletes{ get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
