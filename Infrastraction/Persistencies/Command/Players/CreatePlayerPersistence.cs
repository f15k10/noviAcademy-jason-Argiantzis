using NoviCode.Application.Interfaces;
using NoviCode.Domain.Entity;
using NoviCode.Infrastructure.Persistencies.Context;

namespace NoviCode.Infrastructure.Persistencies.Command.Players
{
    public class CreatePlayerPersistence : ICreatePlayerPersistence
    {
        private readonly WorldRankDbContext _db;

        public CreatePlayerPersistence(WorldRankDbContext db)
        {
            _db = db;
        }

        public async Task Persist(Player player)
        {
            _db.Add(player);
            await _db.SaveChangesAsync();
        }
    }
}
