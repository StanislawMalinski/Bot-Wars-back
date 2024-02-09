using Shared.DataAccess.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Gameplay.Interface
{
    public interface IGameManager
    {
        GameResult PlayGame(Game game, List<Bot> bots);
    }
}
