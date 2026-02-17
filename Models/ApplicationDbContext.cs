using Microsoft.EntityFrameworkCore;

namespace GrowTree.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        
        public DbSet<UserPackage> UserPackages { get; set; }
        public DbSet<UserTree> UserTree { get; set; }




    }
}
