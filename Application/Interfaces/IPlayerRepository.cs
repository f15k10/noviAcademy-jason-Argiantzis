using NoviCode.Domain.Entity;

namespace Application.Repositories
{
	public interface IPlayerRepository
	{
		Task AddAsync(Player player, CancellationToken cancellationToken=default);

		Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken=default);

		//void DeletePlayer(int playerId);

		Task<Player?> GetByIdAsync(int playerId,CancellationToken cancellationToken= default);
        Task<Player?> GetByNameAsync(String name, CancellationToken cancellationToken = default);

        //IEnumerable<IGrouping<int, Player>> GroupPlayersByScore();
    }
}