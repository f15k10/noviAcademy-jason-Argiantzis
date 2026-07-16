using Application.Strategies;
using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;

namespace NoviCode.Application.Service
{
    public interface IWalletService
    {
        Task<Wallet> CreateWalletAsync(int playerId, Currency currency, CancellationToken cancellationToken= default);
        Task<Wallet?> GetByIdAsync(int playerId, CancellationToken cancellationToken= default);
        Task<IReadOnlyList<Wallet>> GetByPlayerAsync(int playerId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Wallet>> GetAllAsync( CancellationToken cancellationToken = default);

        Task ApplyFundsAsync(Wallet wallet, decimal amount , IFundsStrategy strategy, CancellationToken cancellationToken = default);

        Task<Wallet?> DespositAsync(int walletId , decimal amount,CancellationToken cancellationToken = default);

        Task SetBlockedAsync(Wallet wallet,bool blocked,CancellationToken cancellationToken=default);
    }
}
