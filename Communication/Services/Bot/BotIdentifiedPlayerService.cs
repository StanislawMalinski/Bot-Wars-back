using Shared.DataAccess.DTO.Requests;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotIdentifiedPlayerService : BotUnidentifiedPlayerService
{
    public BotIdentifiedPlayerService(BotServiceProvider botServiceProvider) : base(botServiceProvider)
    {
    }
    
    public override async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        return await _botServiceProvider.AddBot(botRequest);
    }

    public override async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        return await _botServiceProvider.DeleteBot(botId);
    }
}