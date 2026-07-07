using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class InMemoryWalletRepository : IWalletRepository
    {




        public void AddWallet(Wallet wallet, int playerId)
        {
           
        }

        public List<Wallet> GetByPlayer(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
