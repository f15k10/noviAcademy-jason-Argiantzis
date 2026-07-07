using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    interface IWalletRepository
    {
        void AddWallet(Wallet wallet, int playerId);
        List<Wallet> GetByPlayer(int playerId);

    }
}
