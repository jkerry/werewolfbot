using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerewolfGame.Factory
{
    public interface IGameService
    {
        Game GetActiveGame();
        bool EndActiveGame();
    }
}
