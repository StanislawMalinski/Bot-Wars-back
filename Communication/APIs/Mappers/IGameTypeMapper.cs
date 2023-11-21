using Communication.APIs.DTOs;

namespace BotWars.GameTypeData
{
    public interface IGameTypeMapper
    {
        public GameTypeDto ToDto(GameType gameType);
        public GameType ToGameType(GameTypeDto gameTypeDto);
    }
}
