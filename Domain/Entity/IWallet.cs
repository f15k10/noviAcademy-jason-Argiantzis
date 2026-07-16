using NoviCode.Domain.Enums;

namespace NoviCode.Domain.Entity
{
	public interface IWallet
	{
		int PlayerId { get; }
		Currency Currency { get; }
		decimal Balance { get; }
		bool IsBlocked { get; }

		void Block();
		void Unblock();
		void SetBalance(decimal balance);
		void Deposit(decimal amount);
		void Withdraw(decimal amount);
	}
}
