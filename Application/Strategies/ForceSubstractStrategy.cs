using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
