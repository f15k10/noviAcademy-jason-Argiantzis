using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public interface IPlayerRepository
    {
        void AddPlayer(Player p);
        Player FindPlayer(int playerId);
        void DeletePlayer(int playerId);

        void GroupPlayersByScore(int score);
    }
}
