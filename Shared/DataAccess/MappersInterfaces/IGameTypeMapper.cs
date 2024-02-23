using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Shared.DataAccess.Mappers
{
	public interface IGameTypeMapper
    {
        public GameDto ToDto(Game gameType);
        public Game ToGameType(GameDto gameTypeDto);
    }
}
