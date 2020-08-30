using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public sealed class SqliteDataContext : DataContext
    {
        public SqliteDataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
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
