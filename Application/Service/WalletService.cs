using Application.Strategies;
using Microsoft.Extensions.Logging;
using NoviCode.Application.Cache;
using NoviCode.Application.Interfaces;
using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;

namespace NoviCode.Application.Service
{
    public class WalletService : IWalletService
    {
        private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(60);

        private readonly IWalletRepository _walletRepository;
        private readonly ICache _cache;
        private readonly ILogger<WalletService> _logger;

        public WalletService(IWalletRepository walletRepository, ICache cache, ILogger<WalletService> logger)
        {
            _walletRepository = walletRepository;
            _cache = cache;
            _logger = logger;
        }

        private static string WalletKey(int id) => $"wallet:{id}";
        private static string PlayerWalletsKey(int idPlayer) => $"wallets:player:{idPlayer}";
        private const string AllWalletsKey = "wallets:all";


       public async Task<Wallet> CreateWalletAsync(int playerId, Currency currency, CancellationToken cancellationToken= default)
        {
            var wallet = new Wallet(playerId, currency);
            await _walletRepository.AddAsync(wallet, cancellationToken);
            _logger.LogInformation("Wallet created {WalletId} for player {PlayerId} in {Currency}", wallet.Id, playerId, currency);
            Refresh(wallet);
            return wallet;

        }
       public async Task<Wallet?> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
        {
            if(_cache.TryGet(WalletKey(Id),out Wallet? cached) && cached is not null)
            {
                _logger.LogInformation("Cache HIT wallet {WalletId}", Id);
                return cached;
            }
            _logger.LogInformation("Cache MISS wallet {WalletId} ", Id);
            var wallet  = await _walletRepository.GetByIdAsync(Id, cancellationToken);
            if(wallet is not null)
            { _cache.Set(WalletKey(Id), wallet, Ttl); }
            return wallet;
        }
        public async Task<IReadOnlyList<Wallet>> GetByPlayerAsync(int playerId, CancellationToken cancellationToken = default)
        {
            if (_cache.TryGet(PlayerWalletsKey(playerId), out IReadOnlyList<Wallet>? cached) && cached is not null)
            {
                _logger.LogInformation("Cache HIT wallet {WalletId}", playerId);
                return cached;
            }
            _logger.LogInformation("Cache MISS wallet {WalletId} ", playerId);
            var wallets = await _walletRepository.GetByPlayerAsync(playerId, cancellationToken);
  
             _cache.Set(PlayerWalletsKey(playerId), wallets, Ttl); 
            return wallets;
        }

        public async Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (_cache.TryGet(AllWalletsKey, out IReadOnlyList<Wallet>? cached) && cached is not null)
            {
                _logger.LogInformation("Cache HIT all wallets");
                return cached;
            }
            _logger.LogInformation("Cache MISS wallets ");
            var wallets = await _walletRepository.GetAllAsync(cancellationToken);

            _cache.Set(AllWalletsKey, wallets, Ttl);
            return wallets;
        }

       public async Task ApplyFundsAsync(Wallet wallet, decimal amount, IFundsStrategy strategy, CancellationToken cancellationToken = default)
        {
            strategy.Execute(wallet, amount);
            await _walletRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Applied {Stratagy} , {Amount} to wallet {WalletId}; new balance {Balance}", strategy.GetType().Name
                , amount, wallet.Id, wallet.Balance);
            
        }

        public async Task<Wallet?> DespositAsync(int walletId, decimal amount, CancellationToken cancellationToken = default)
        {
            var wallet = await _walletRepository.GetByIdAsync(walletId,cancellationToken);

            if (wallet is null)
                return null;
            wallet.Deposit(amount);
            await _walletRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deposited {Amount} to wallet {WalletId}", amount, walletId);
            Refresh(wallet);
            return wallet;
        }

        public async Task SetBlockedAsync(Wallet wallet, bool blocked, CancellationToken cancellationToken = default)
        {
            if (blocked)
                wallet.Block();
            else
                wallet.Unblock();
            await _walletRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Wallet {WalletId} is now {State}", wallet.Id, blocked ? "blocked" : "active");
            Refresh(wallet);
        }
        private void Refresh(Wallet wallet)
        {
            _cache.Set(WalletKey(wallet.Id), wallet, Ttl);
            _cache.Remove(AllWalletsKey);
            _cache.Remove(PlayerWalletsKey(wallet.PlayerId));
            _logger.LogInformation("Cache write-through wallet {WalletId}; list caches invalidated", wallet.Id);

        }
    }
}
