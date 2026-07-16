using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;


namespace Application.Strategies
{
    public class SubstractFundsStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.Subtract;

        public void Execute(Wallet wallet, decimal amount)
        {
            wallet.Withdraw(amount);
        }
    }
}
