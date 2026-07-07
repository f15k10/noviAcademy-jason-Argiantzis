using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        public List<Player> players = new List<Player>();
        public InMemoryPlayerRepository(List<Player> p)
        {
           this.players=p;
        }

        public void AddPlayer(Player p)
        {
            //Do the checks here to see if the player already exists in the list, if not add it to the list
            players.Add(p);
        }
        public Player FindPlayer(int playerId)
        {
            //Find the player by id and return it
            return players.Find(p => p.Id.Equals(playerId));
        }

        public void DeletePlayer(int playerId)
        {

        }
        void GroupPlayersByScore(int score)
        {

        }

        void IPlayerRepository.GroupPlayersByScore(int score)
        {
            GroupPlayersByScore(score);
        }
    }
}
