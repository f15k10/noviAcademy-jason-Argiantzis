using NoviCode.Domain.Enums;

namespace NoviCode.Domain.Exceptions
{
	public class WalletBlockedException : WalletException
	{
		public Currency Currency { get; }

		public WalletBlockedException(Currency currency)
			: base($"The {currency} wallet is blocked. Unblock it before performing this operation.")
		{
			Currency = currency;
		}
	}
}
