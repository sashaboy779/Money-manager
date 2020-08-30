using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer.Migrations
{
    internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = MigrationHelper.GetConnectionString();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
