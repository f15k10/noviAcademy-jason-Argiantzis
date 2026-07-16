using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;


namespace Application.Strategies
{
    public class AddFundsStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.Add;

        public void Execute(Wallet wallet, decimal amount)
        {
            wallet.Deposit(amount);
        }
    }
}
