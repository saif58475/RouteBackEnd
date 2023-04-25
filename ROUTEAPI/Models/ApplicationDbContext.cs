global using Microsoft.EntityFrameworkCore;

namespace ROUTEAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities{ get; set; }
        public DbSet<District> Districts{ get; set; }
        public DbSet<Block> Blocks{ get; set; }
    }
}
