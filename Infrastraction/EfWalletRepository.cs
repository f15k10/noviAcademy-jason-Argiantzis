using Microsoft.EntityFrameworkCore;
using NoviCode.Application.Interfaces;
using NoviCode.Domain.Entity;
using NoviCode.Infrastructure.Persistencies.Context;

namespace NoviCode.Infrastructure
{
    public class EfWalletRepository : IWalletRepository
    {

        private readonly WorldRankDbContext _context;

        public EfWalletRepository(WorldRankDbContext context) { _context = context; }


        public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
        {
            await _context.Wallets.AddAsync(wallet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Wallet?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Wallets.FirstOrDefaultAsync(w => w.Id == id,cancellationToken);
        }
        public async Task<IReadOnlyList<Wallet>> GetByPlayerAsync(int id, CancellationToken cancellationToken = default)
        => await _context.Wallets.Where(w => w.PlayerId == id).ToListAsync(cancellationToken);
        public async Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Wallets.AsNoTracking().ToListAsync(cancellationToken);
        public  Task SaveChangesAsync(CancellationToken cancellationToken = default)
        =>  _context.SaveChangesAsync(cancellationToken);
    }
}
