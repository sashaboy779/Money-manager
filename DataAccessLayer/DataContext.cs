using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MainCategory> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainCategory>()
               .HasMany(s => s.Subcategories)
               .WithOne(c => c.Parent)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
