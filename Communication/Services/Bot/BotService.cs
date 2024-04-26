using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotService : IBotService
{
    private readonly IBotRepository _botRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextRepository _userContextRepository;

    public BotService(IBotRepository botRepository,
        IAuthorizationService authorizationService,
        IUserContextRepository userContextRepository)
    {
        _userContextRepository = userContextRepository;
        _authorizationService = authorizationService;
        _botRepository = botRepository;
    }
    

    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        return await _botRepository.GetBotResponse(botId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest, long playerId)
    {
        return await _botRepository.AddBot(botRequest, playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        var botResult = await _botRepository.GetBot(botId);
        if (botResult.IsError)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Bot o podanym id nie istnieje"
            };
        }
        var bot = botResult.Match(x=>x.Data,x => null);
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            bot,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
        
        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        return await _botRepository.DeleteBot(botId);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId, PageParameters pageParameters)
    {
        return await _botRepository.GetBotsForPlayer(playerId, pageParameters);
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetBotFileForPlayer(long playerId,
        long botId)
    {
        var botResult = await _botRepository.GetBot(botId);
        if (botResult.IsError)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Bot o podanym id nie istnieje"
            };
        }
        var bot = botResult.Match(x=>x.Data,x => null);
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            bot,
            new ResourceOperationRequirement(ResourceOperation.ReadRestricted)).Result;
        
        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        return await _botRepository.GetBotFileForPlayer(botId);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForTournament(long tournamentId, PageParameters pageParameters)
    {
        return await _botRepository.GetBotsForTournament(tournamentId, pageParameters) ;
    }
}