using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank
{
    public class InMemoryPlayerRepository : IPlayerRepository
    {
        public List<Player> _players = new List<Player>();
        public InMemoryPlayerRepository(List<Player> p)
        {
           _players=p;
        }

        public void AddPlayer(Player p)
        {
            //Do the checks here to see if the player already exists in the list, if not add it to the list
            _players.Add(p);
        }
        public Player FindPlayer(int playerId)
        {
            //Find the player by id and return it
            return _players.Where(item => item.Id == playerId).FirstOrDefault();
        }

        public void DeletePlayer(int playerId)
        {
            var player = _players.Where(item => item.Id == playerId).FirstOrDefault();

            if (player != null)
            {
                _players.Remove(player);
            }
        }
       public  void GroupPlayersByScore(int score)
        {
            var v = _players.GroupBy(item => score);

            foreach (var yearGroup in v)
            {
                Console.WriteLine($"Key: {yearGroup.Key}");
                foreach (var student in yearGroup)
                {
                    Console.WriteLine($"\t{student}");

                }
            }
        }

    }
}
