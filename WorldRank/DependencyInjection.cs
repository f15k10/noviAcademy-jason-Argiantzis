using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WorldRank.Infrastructure;
using WorldRank.Infrastructure.Persistencies.Context;

namespace WorldRank.Console
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorldRank(this IServiceCollection services)
        {
            services.AddDbContext<WorldRankDbContext>(options =>
                options.UseSqlServer(
                    "Server=localhost;Database=WorldRankDb;Trusted_Connection=True;TrustServerCertificate=True;"));

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddNLog();
            });

            services.AddApplication();
            services.AddInfrastructure();

            return services;
        }
    }
}
