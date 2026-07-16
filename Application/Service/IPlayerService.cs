using NoviCode.Domain.Entity;

namespace NoviCode.Application.Service
{
    public interface IPlayerService
    {
        Task<Player> CreateAsync(string name, int score, CancellationToken cancellationToken = default);
        Task<Player?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
