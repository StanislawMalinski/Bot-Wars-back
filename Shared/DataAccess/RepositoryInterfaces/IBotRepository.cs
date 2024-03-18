using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotRepository
{
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots();
    Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId);
    Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest);
    Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId);
}