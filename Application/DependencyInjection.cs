using NoviCode.Application.Service;
using Application.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace NoviCode.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // All strategies are registered under the same interface. The caller resolves
            // them as a collection and picks the one whose Operation matches - no factory.
            services.AddSingleton<IFundsStrategy, AddFundsStrategy>();
            services.AddSingleton<IFundsStrategy, SubstractFundsStrategy>();
            services.AddSingleton<IFundsStrategy, ForceSubstractStrategy>();

            // Application services that drive the menu use-cases.
            services.AddSingleton<PlayerService>();
            services.AddSingleton<WalletService>();

            return services;
        }
    }
}
