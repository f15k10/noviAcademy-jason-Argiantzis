using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;


namespace Application.Strategies
{
    public class ForceSubstractStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.ForceSubstract;

        public void Execute(Wallet wallet, decimal amount)
        {
            wallet.ForceWithdraw(amount);
        }
    }
}
