using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfGame.Factory
{
    public static class GameFactory
    {

        public static Game GenerateGame()
        {
            return new Game();
        }
    }
}
