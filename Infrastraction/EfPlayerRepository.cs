using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using NoviCode.Domain.Entity;
using NoviCode.Infrastructure.Persistencies.Context;

namespace NoviCode.Infrastructure
{
    public class EfPlayerRepository : IPlayerRepository
    {
        private readonly WorldRankDbContext _context;

        public EfPlayerRepository(WorldRankDbContext context) { _context = context; }


        public async Task AddAsync(Player player, CancellationToken cancellationToken = default)
        {
            await _context.Players.AddAsync(player, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Player?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public Task<Player?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        => _context.Players.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

        public async Task<IReadOnlyList<Player>>  GetAllAsync(CancellationToken cancellationToken = default)
        =>await _context.Players.AsNoTracking().ToListAsync(cancellationToken);
    }
}
