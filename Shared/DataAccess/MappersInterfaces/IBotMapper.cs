using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public interface IBotMapper
{
    Bot MapRequestToBot(BotRequest botRequest);
    BotResponse MapBotToResponse(Bot bot);
}