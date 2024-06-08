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

namespace Communication.Services.GameType;

public class GameTypeService : IGameService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IFileRepository _fileRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameTypeMapper _gameTypeMapper;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserContextRepository _userContextRepository;

    public GameTypeService(IGameRepository gameRepository,
        IAuthorizationService authorizationService,
        IUserContextRepository userContextRepository, IGameTypeMapper gameTypeMapper,
        IPlayerRepository playerRepository, IFileRepository fileRepository)
    {
        _userContextRepository = userContextRepository;
        _gameTypeMapper = gameTypeMapper;
        _playerRepository = playerRepository;
        _authorizationService = authorizationService;
        _gameRepository = gameRepository;
        _fileRepository = fileRepository;
    }

    public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
    {
        var res = await _gameRepository.GetGameIncluded(id);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<GameResponse>
        {
            Data = _gameTypeMapper.MapGameToResponse(res)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> Search(string? name,
        PageParameters pageParameters)
    {
        var res = await _gameRepository.Search(name, pageParameters);
        return new SuccessData<PageResponse<GameResponse>>
        {
            Data = new PageResponse<GameResponse>(res, pageParameters.PageSize, res.Count)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest)
    {
        var gameCreatorId = await _gameRepository.GetCreatorId(id);
        if (gameCreatorId is null)
            return new EntityNotFoundErrorResult
            {
                Title = "Return null",
                Message = "Typ gry o podanym id nie istnieje"
            };
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            gameCreatorId,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded) return new UnauthorizedError();
        var res = await _gameRepository.ModifyGameType(id, gameRequest);
        if (!res) return new EntityNotFoundErrorResult();
        await _gameRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        var gameCreatorId = await _gameRepository.GetCreatorId(id);
        if (gameCreatorId is null)
            return new EntityNotFoundErrorResult
            {
                Title = "Return null",
                Message = "Typ gry o podanym id nie istnieje"
            };
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            gameCreatorId,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded) return new UnauthorizedError();

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

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGames(
        PageParameters pageParameters)
    {
        var res = await _gameRepository.GetGames(pageParameters);
        return new SuccessData<PageResponse<GameResponse>>
        {
            Data = new PageResponse<GameResponse>(res, pageParameters.PageSize, res.Count)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>>
        GetListOfTypesOfAvailableGames(
            PageParameters pageParameters)
    {
        var res = await _gameRepository.GetAvailableGames(pageParameters);
        return new SuccessData<PageResponse<GameResponse>>
        {
            Data = new PageResponse<GameResponse>(res, pageParameters.PageSize, res.Count)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGamesByPlayer(
        string? name, PageParameters pageParameters)
    {
        var res = await _playerRepository.GetPlayerByLogin(name);
        if (res == null) return new EntityNotFoundErrorResult();
        var botList = await _gameRepository.GetGamesByPlayer(res.Id, pageParameters);
        return new SuccessData<PageResponse<GameResponse>>
        {
            Data = new PageResponse<GameResponse>(botList, pageParameters.PageSize, botList.Count)
        };
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetGameFile(long id)
    {
        var res = await _gameRepository.GetGameIncluded(id);
        if (res == null) return new EntityNotFoundErrorResult();
        var log = await _fileRepository.GetFile(res.FileId, res.GameFile);

        if (!log.IsSuccess)
        {
            Console.WriteLine("Game file not found in File Gatherer");
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "Game file not found in File Gatherer"
            };
        }

        return new SuccessData<IFormFile>
        {
            Data = log.Match(x => x.Data, null!)
        };
    }
}