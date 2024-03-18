using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotServiceProvider : IBotService
{
    private readonly IBotRepository _botRepository;

    public BotServiceProvider(IBotRepository botRepository)
    {
        _botRepository = botRepository;
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots()
    {
        return await _botRepository.GetAllBots();
    }

    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        return await _botRepository.GetBotResponse(botId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        return await _botRepository.AddBot(botRequest);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        return await _botRepository.DeleteBot(botId);
    }
}