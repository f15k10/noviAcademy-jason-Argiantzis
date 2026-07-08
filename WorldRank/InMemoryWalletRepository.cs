using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class InMemoryWalletRepository : IWalletRepository
    {

        private List<Player> _players;

        public InMemoryWalletRepository(List<Player> players)
        {
            _players = players;
        }

        public void AddWallet(Wallet wallet, int playerId)
        {
            var player = _players.Where(item => item.Id == playerId).SingleOrDefault();

            if (player != null)
            {
                player._wallet.Add(wallet.Currency, wallet);
            }
        }

        public List<Wallet> GetByPlayer(int playerId)
        {
            throw new NotImplementedException();
        }
    }
}
