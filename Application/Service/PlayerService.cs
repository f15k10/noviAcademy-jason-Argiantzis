using Application.Repositories;
using Microsoft.Extensions.Logging;
using NoviCode.Application.Cache;
using NoviCode.Domain.Entity;

namespace NoviCode.Application.Service
{
    public class PlayerService : IPlayerService
    {
        private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(60);

        private readonly IPlayerRepository _playerRepository;
        private readonly ICache _cache;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IPlayerRepository playerRepository, ICache cachel, ILogger<PlayerService> logger)
        {
            _playerRepository = playerRepository;
            _cache = cachel;
            _logger = logger;
        }
        private static string PlayerKey(int id) => $"{id}";
        private const string AllPlayersKey = "players:all";

        public async Task<Player> CreateAsync(string name,int score, CancellationToken cancellationToken=default)
        {
            var player = new Player(name);
            player.AddScore(score);

            await _playerRepository.AddAsync(player, cancellationToken); //DB
            _logger.LogInformation("Player created {PlayerId} {Name} (score {Score})", player.Id, name, score);

            _cache.Set(PlayerKey(player.Id), player, Ttl);
            _cache.Remove(AllPlayersKey);
            _logger.LogInformation("Cache write-through player {PlayerId}; list cache invalidated", player.Id);
            return player;
        }

        public async Task<Player?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if(_cache.TryGet(PlayerKey(id),out Player? cached) && cached is not null)
            {
                _logger.LogInformation("Cache HIT player {PlayerId}", id);
                return cached;
            }

            _logger.LogInformation("Cache MISS player {PlayerId} - loading from db", id);
            var player = await _playerRepository.GetByIdAsync(id, cancellationToken);
            if(player is not null)
            {
                _cache.Set(PlayerKey(id), player, Ttl);
            }
            return player;
        }


        public async Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (_cache.TryGet(AllPlayersKey, out IReadOnlyList<Player>? cached) && cached is not null)
            {
                _logger.LogInformation("Cache HIT All players ");
                return cached;
            }

            _logger.LogInformation("Cache MISS players");
            var player = await _playerRepository.GetAllAsync(cancellationToken);
      
                _cache.Set(AllPlayersKey, player, Ttl);
   
            return player;
        }
    }
}
