using Application.Repositories;
using Microsoft.Extensions.Logging;
using NLog;
using WorldRank.Domain.Entity;
using WorldRank.Infrastructure.Persistencies.Context;


namespace WorldRank.Infrastructure
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{
        private readonly WorldRankDbContext _context;
        private  readonly ILogger<InMemoryPlayerRepository> _logger;

		private List<Player> _players = new();

		public InMemoryPlayerRepository(WorldRankDbContext context, ILogger<InMemoryPlayerRepository> logger)
		{
            _context = context;
            _logger = logger;
		}

		public void AddPlayer(Player player)
		{
			_players.Add(player);

            _context.Players.Add(player);
            _context.SaveChanges();
            _logger.LogInformation("Player {PlayerId} ({Name}) added with score {Score}", player.Id, player.Name, player.Score);
		}

		public IEnumerable<Player> GetAllPlayers()
		{
            // Return a copy so callers cannot mutate the repository's internal list.
			//Now i return from the db
            return _context.Players.ToList();
        }

		public void DeletePlayer(int playerId)
		{
            var player = _context.Players
               .FirstOrDefault(p => p.Id == playerId);

            if (player is null)
            {
                _logger.LogWarning(
                    "Delete skipped: player {PlayerId} not found",
                    playerId);
                return;
            }

            _context.Players.Remove(player);
            _context.SaveChanges();

            _logger.LogInformation(
                "Player {PlayerId} deleted",
                playerId);
        }

		public Player? FindPlayer(int playerId)
		{
            return _context.Players
               .FirstOrDefault(p => p.Id == playerId);
        }

		public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
		{
            return _context.Players
               .AsEnumerable()
               .GroupBy(player => player.Score)
               .OrderByDescending(group => group.Key);
        }
	}
}
