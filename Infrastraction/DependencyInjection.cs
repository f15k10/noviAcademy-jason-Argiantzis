using Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Persistencies.Context;


namespace WorldRank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext(<WorldRankDbContext> (options =>
               options.UseSqlServer("");
            // In-memory repositories hold state, so they must live for the whole app (Singleton).
            services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
            services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();

            return services;
        }
    }
}
