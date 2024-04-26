using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotService : IBotService
{
    private readonly IBotRepository _botRepository;

    public BotService(IBotRepository botRepository)
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

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest,long playerId)
    {
        return await _botRepository.AddBot(botRequest,playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        return await _botRepository.DeleteBot(botId);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId)
    {
        return await _botRepository.GetBotsForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetBotFileForPlayer(long playerId, long botId)
    {
        return await _botRepository.GetBotFileForPlayer(playerId, botId);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForTournament(long tournamentId)
    {
        return await _botRepository.GetBotsForTournament(tournamentId) ;
    }
}