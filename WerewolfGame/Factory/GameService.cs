using System;
using System.Collections.Generic;
using System.Linq;

namespace WerewolfGame.Factory
{
    class GameService : IGameService
    {
        private static IList<Game> games = new List<Game>();
        private static object lockobject = new object();

        public bool EndActiveGame()
        {
            try
            {
                lock (lockobject)
                {
                    foreach (var game in games)
                    {
                        game.Dispose();
                    }
                    games.Clear();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Game GetActiveGame()
        {
            lock (lockobject)
            {
                if (games.Count != 1)
                {
                    games.Clear();
                    games.Add(GameFactory.GenerateGame());
                }
                return games.First();
            }
        }
    }
}
