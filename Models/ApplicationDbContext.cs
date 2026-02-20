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

        // 👇 SQL View
        public DbSet<MyDirectViewModel> MyDirect { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Map SQL View
            modelBuilder.Entity<MyDirectViewModel>(entity =>
            {
                entity.HasNoKey(); // View has no primary key
                entity.ToView("vw_GetMyDirect"); // SQL View name
            });
        }


    }
}
