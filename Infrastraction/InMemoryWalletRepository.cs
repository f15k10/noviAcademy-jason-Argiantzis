using NoviCode.Application.Interfaces;
using NoviCode.Domain.Entity;



namespace NoviCode.Infrastructure
{
    public class InMemoryWalletRepository : IWalletRepository
    {
        private readonly List<Wallet> _wallets = new();

        public Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            _wallets.Add(wallet);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Wallet>>(_wallets.ToList());

        public Task<Wallet?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => Task.FromResult(_wallets.FirstOrDefault(w=>w.Id== id));

        public Task<IReadOnlyList<Wallet>> GetByPlayerAsync(int id_player, CancellationToken cancellationToken = default)
      => Task.FromResult<IReadOnlyList<Wallet>>(_wallets.Where(w => w.PlayerId == id_player).ToList());

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    }
}
