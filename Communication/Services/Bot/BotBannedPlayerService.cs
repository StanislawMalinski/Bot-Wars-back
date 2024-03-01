using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotBannedPlayerService : IBotService
{
    public virtual async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots()
    {
        return new AccessDeniedError();
    }

    public virtual async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        return new AccessDeniedError();
    }

    public virtual async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        return new AccessDeniedError();
    }

    public virtual async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        return new AccessDeniedError();
    }
}