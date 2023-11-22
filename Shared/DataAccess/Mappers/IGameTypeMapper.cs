using Communication.APIs.DTOs;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers
{
    public interface IGameTypeMapper
    {
        public GameTypeDto ToDto(GameType gameType);
        public GameType ToGameType(GameTypeDto gameTypeDto);
    }
}
