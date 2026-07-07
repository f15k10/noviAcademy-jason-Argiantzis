using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class Wallet
    {
        public decimal Balance { get; private set; }
        public Currency Currency { get;   set; }
        public bool IsBlocked { get;  set; }

        public Wallet(decimal initialBalance, Currency currency_const,bool isBlocked)
        {
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance must be non-negative.");
            Balance = initialBalance;
            Currency = currency_const;
            IsBlocked = false;
        }
    }
}
