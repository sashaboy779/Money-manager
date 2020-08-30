using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer.Migrations
{
    internal class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteDataContext>
    {
        public SqliteDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqliteDataContext>();
            var connectionString = MigrationHelper.GetConnectionString();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite(connectionString);

            return new SqliteDataContext(optionsBuilder.Options);
        }
    }
}
