using BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class SuccessfullGameResult : GameResult
{
    public Bot BotWinner { get; set; }
    public Bot BotLoser { get; set; }
}