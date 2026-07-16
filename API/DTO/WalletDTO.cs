using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;

namespace NoviCode.API.DTO
{
    public record  CreateWalletRequest(int PlayerId, Currency currency);

    public record DepositRequest(decimal Amount);
    public record WalletResponse(int id,int playerId, Currency currency,decimal Balance, bool IsBlocked)
    {
        public static WalletResponse From(Wallet wallet)
            => new(wallet.Id,wallet.PlayerId,wallet.Currency,wallet.Balance,wallet.IsBlocked);
    }
}
