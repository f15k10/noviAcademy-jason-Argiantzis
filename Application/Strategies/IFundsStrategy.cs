using NoviCode.Domain.Entity;
using NoviCode.Domain.Enums;


namespace Application.Strategies
{
    public interface IFundsStrategy
    {
        FundsOperation Operation { get; }

        void Execute(Wallet wallet , decimal amount)
        {

        }
    }
}
