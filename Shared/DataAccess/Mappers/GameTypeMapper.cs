using Communication.APIs.DTOs;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers
{
    public class GameTypeMapper : IGameTypeMapper
    {
        public GameDto ToDto(Game gameType)
        {
            //return new GameDto { name = gameType.Name, isAvialable = gameType.IsAvialable };
            return null;

        }

        public Game ToGameType(GameDto gameDto)
        {
           // return new GameType { Name = gameDto.name, IsAvialable = gameDto.isAvialable };
           return null;

        }

    }
}
