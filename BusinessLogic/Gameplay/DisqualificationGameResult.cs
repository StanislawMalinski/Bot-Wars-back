using BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay
{
    class DisqualificationGameResult : GameResult
    {
        public Bot DisqualifiedBot { get; set; }
        public DisqualificationGameStatus Status { get; set; } 
    }
}
