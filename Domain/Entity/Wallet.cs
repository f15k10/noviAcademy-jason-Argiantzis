using NoviCode.Domain.Enums;
using NoviCode.Domain.Exceptions;

namespace NoviCode.Domain.Entity
{
	public class Wallet : IWallet
	{
		public Currency Currency { get; }
		public int Id { get; }
		public int PlayerId { get; }
		public decimal Balance { get; private set; }
		public bool IsBlocked { get; private set; }
        private static readonly Random Random = new();
		//This const is for unit test only
        public Wallet(int id, int playerId, Currency currency,decimal balance, bool isBlocked = false)
        {
            Id = id;
            PlayerId = playerId;
            Currency = currency;
			Balance = balance;
            IsBlocked = isBlocked;
        }
        public Wallet(int playerId, Currency currency, bool isBlocked = false)
		{
			Id= GenerateRandomInt(1, 9999);
            PlayerId = playerId;
			Currency = currency;
			IsBlocked = isBlocked;
		}
     
        public static int GenerateRandomInt(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
        public void Block() => IsBlocked = true;

		public void Unblock() => IsBlocked = false;

		public void SetBalance(decimal balance)
		{
			if (balance < 0)
				throw new InsufficientFundsException(balance);

			Balance = balance;
		}

		public void Deposit(decimal amount)
		{
			if (amount <= 0)
				throw new InvalidAmountException(amount);

			if (IsBlocked)
				throw new WalletBlockedException(Currency);

			Balance += amount;
		}

		public void Withdraw(decimal amount)
		{
			if (amount <= 0)
				throw new InvalidAmountException(amount);

			if (IsBlocked)
				throw new WalletBlockedException(Currency);

			var newBalance = Balance - amount;
			if (newBalance < 0)
				throw new InsufficientFundsException(newBalance);

			Balance = newBalance;
		}

        public void ForceWithdraw(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException(amount);

            if (IsBlocked)
                throw new WalletBlockedException(Currency);

            var newBalance = Balance - amount;
            Balance = newBalance;
        }

        public override string ToString() => $"Balance -> {Balance} Currency -> {Currency} IsBlocked -> {IsBlocked}";
	}
}
