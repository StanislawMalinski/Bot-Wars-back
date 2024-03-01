using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotUnidentifiedPlayerService : BotBannedPlayerService
{
    protected readonly BotServiceProvider _botServiceProvider;

    public BotUnidentifiedPlayerService(BotServiceProvider botServiceProvider)
    {
        _botServiceProvider = botServiceProvider;
    }

    public override async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots()
    {
        return await _botServiceProvider.GetAllBots();
    }
    
    public override async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        return await _botServiceProvider.GetBotResponse(botId);
    }
}