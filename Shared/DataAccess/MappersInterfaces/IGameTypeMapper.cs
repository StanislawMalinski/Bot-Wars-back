using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public interface IGameTypeMapper
{
    Game MapRequestToGame(GameRequest gameRequest);
    GameResponse MapGameToResponse(Game game);
    GameSimpleResponse MapGameToSimpleResponse(Game game);
}