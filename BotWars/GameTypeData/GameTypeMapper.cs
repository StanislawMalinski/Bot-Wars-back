using BotWars.RockPaperScissorsData;

namespace BotWars.GameTypeData
{
    public class GameTypeMapper : IGameTypeMapper
    {
        public GameTypeDto ToDto(GameType gameType)
        {
            return new GameTypeDto { name = gameType.Name, isAvialable = gameType.IsAvialable };
        }

        public GameType ToGameType(GameTypeDto gameTypeDto)
        {
            return new GameType { Name = gameTypeDto.name, IsAvialable = gameTypeDto.isAvialable };
        }

    }
}
