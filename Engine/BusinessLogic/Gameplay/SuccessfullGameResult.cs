using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay;

public class SuccessfullGameResult : GameResult
{
  public Bot BotWinner { get; set; }
  public Bot BotLoser { get; set; }
}