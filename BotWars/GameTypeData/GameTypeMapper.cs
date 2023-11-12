//using BotWars.RockPaperScissorsData;

namespace BotWars.GameTypeData
{
    public class GameTypeMapper : IGameTypeMapper
    {
        public GameTypeDto toDto(GameType gameType)
        {
            return new GameTypeDto { name = gameType.Name, isAvialable = gameType.IsAvialable };
        }
    }
}
