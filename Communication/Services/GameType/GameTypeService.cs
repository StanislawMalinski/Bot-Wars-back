using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Pagination;
using Shared.Results.ErrorResults;

namespace Communication.Services.GameType;

public class GameTypeService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextRepository _userContextRepository;
    private readonly IGameTypeMapper _gameTypeMapper;
    private readonly IPlayerRepository _playerRepository;

    public GameTypeService(IGameRepository gameRepository,
        IAuthorizationService authorizationService,
        IUserContextRepository userContextRepository, IGameTypeMapper gameTypeMapper, IPlayerRepository playerRepository)
    {
        _userContextRepository = userContextRepository;
        _gameTypeMapper = gameTypeMapper;
        _playerRepository = playerRepository;
        _authorizationService = authorizationService;
        _gameRepository = gameRepository;
    }

    public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
    {
        var res = await _gameRepository.GetGameIncluded(id);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<GameResponse>()
        {
            Data = _gameTypeMapper.MapGameToResponse(res)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> Search(string? name, PageParameters pageParameters)
    {
        var res = await _gameRepository.Search(name, pageParameters);
        return new SuccessData<PageResponse<GameResponse>>()
        {
            Data = new PageResponse<GameResponse>(res, res.Count)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest)
    {
        var gameCreatorId = await _gameRepository.GetCreatorId(id);
        if (gameCreatorId is null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Typ gry o podanym id nie istnieje"
            };
        }
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            gameCreatorId,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        var res = await _gameRepository.ModifyGameType(id, gameRequest);
        if (!res) return new EntityNotFoundErrorResult();
        await _gameRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        var gameCreatorId = await _gameRepository.GetCreatorId(id);
        if (gameCreatorId is null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Typ gry o podanym id nie istnieje"
            };
        }
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            gameCreatorId,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }
        
        await _gameRepository.DeleteGame(id);
        await _gameRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(long? userId, GameRequest gameRequest)
    {
        var res = await _gameRepository.CreateGameType(userId, gameRequest);
        if (res) return new Success();
        return new IncorrectOperation();
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGames(PageParameters pageParameters)
    {
        var res = await _gameRepository.GetGames(pageParameters);
        return new SuccessData<PageResponse<GameResponse>>()
        {
            Data = new PageResponse<GameResponse>(res, res.Count)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames(
        PageParameters pageParameters)
    {
        
        var res = await _gameRepository.GetAvailableGames(pageParameters);
        return new SuccessData<PageResponse<GameResponse>>()
        {
            Data = new PageResponse<GameResponse>(res, res.Count)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name, PageParameters pageParameters)
    {
        var res=  await _playerRepository.GetPlayer(name);
        if (res == null) return new EntityNotFoundErrorResult();
        var botList = await _gameRepository.GetGamesByPlayer(res.Id, pageParameters);
        return new SuccessData<PageResponse<GameResponse>>()
        {
            Data = new PageResponse<GameResponse>(botList, botList.Count)
        };
    }
}