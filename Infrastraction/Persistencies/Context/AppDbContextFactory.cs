using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace NoviCode.Infrastructure.Persistencies.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<WorldRankDbContext>
    {
        public WorldRankDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<WorldRankDbContext>()
                .UseSqlServer(DbConnection.ConnectionString)
                .Options;

            return new WorldRankDbContext(options);
        }
    }
}
