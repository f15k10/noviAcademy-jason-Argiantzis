using NoviCode.Domain.Entity;


namespace NoviCode.Application.Interfaces
{
	public interface IWalletRepository
	{
		Task AddAsync(Wallet wallet, CancellationToken cancellationToken=default);
		Task <Wallet?> GetByIdAsync(int id,CancellationToken cancellationToken=default);
		Task<IReadOnlyList<Wallet>> GetByPlayerAsync(int id_player, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default);

		Task SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
