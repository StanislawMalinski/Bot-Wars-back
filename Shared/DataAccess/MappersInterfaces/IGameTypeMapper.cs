using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers
{
	public interface IGameTypeMapper
    {
        public GameDto ToDto(Game gameType);
        public Game ToGameType(GameDto gameTypeDto);
    }
}
