using Application.Repositories;
using NoviCode.Domain.Entity;



namespace NoviCode.Infrastructure
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{

		private List<Player> _players = new();

		public Task AddAsync(Player player,CancellationToken cancellationToken=default)
		{
            _players.Add(player);
            return Task.CompletedTask;
		}
        public Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Player>>(_players.ToList());

      
        public Task<Player?> GetByIdAsync(int playerId, CancellationToken cancellationToken = default)
               => Task.FromResult(_players.FirstOrDefault(p => p.Id == playerId));
        public Task<Player?> GetByNameAsync(String name, CancellationToken cancellationToken = default)
            => Task.FromResult(_players.FirstOrDefault(p=>p.Name.Equals(name,StringComparison.OrdinalIgnoreCase)));




    }
}
