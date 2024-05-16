using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
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
    private readonly IBotMapper _botMapper;
    private readonly IPlayerRepository _playerRepository;

    public BotService(IBotRepository botRepository, IAuthorizationService authorizationService, IUserContextRepository userContextRepository, IBotMapper botMapper, IPlayerRepository playerRepository)
    {
        _botRepository = botRepository;
        _authorizationService = authorizationService;
        _userContextRepository = userContextRepository;
        _botMapper = botMapper;
        _playerRepository = playerRepository;
    }
    

    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        var res = await _botRepository.GetBot(botId);
        if (res == null)
        {
            return new EntityNotFoundErrorResult();
        }

        return new SuccessData<BotResponse>()
        {
            Data = _botMapper.MapBotToResponse(res)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest, long playerId)
    {
        var res = await _botRepository.AddBot(botRequest, playerId);
        if (res)
        {
            return new Success();
        }
        return new IncorrectOperation();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        var botResult = await _botRepository.GetBot(botId);
        if (botResult == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Bot o podanym id nie istnieje"
            };
        }
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            botResult,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
        
        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        var res =  await _botRepository.DeleteBot(botId);
        if (!res) new EntityNotFoundErrorResult();
        await _botRepository.SaveChangeAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<PageResponse<BotResponse>>, IErrorResult>> GetBotsForPlayer(string? playerName, PageParameters pageParameters)
    {
        if (playerName == null) return new EntityNotFoundErrorResult(); 
        var player = await _playerRepository.GetPlayerByLogin(playerName);
    
        if (player == null) return new EntityNotFoundErrorResult();
        
            
        var bots = await _botRepository.GetPlayerBots(player.Id, pageParameters);

        return new SuccessData<PageResponse<BotResponse>>
        {
            Data = new PageResponse<BotResponse>(bots, bots.Count)
        };
        
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetBotFileForPlayer(long playerId,
        long botId)
    {
        var botResult = await _botRepository.GetBot(botId);
        if (botResult == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Bot o podanym id nie istnieje"
            };
        }
       
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            botResult,
            new ResourceOperationRequirement(ResourceOperation.ReadRestricted)).Result;
        
        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        var res = await _botRepository.GetBotFileForPlayer(botId);
        if (res == null) new EntityNotFoundErrorResult();
        return new SuccessData<IFormFile>()
        {
            Data = res
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<BotResponse>>, IErrorResult>> GetBotsForTournament(long tournamentId, PageParameters pageParameters)
    {
        var res = await _botRepository.GetBotsForTournament(tournamentId, pageParameters) ;
        return new SuccessData<PageResponse<BotResponse>>()
        {
            Data = new PageResponse<BotResponse>(res, res.Count)
        };
    }
}