﻿using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.Results.ErrorResults;

namespace Communication.Services.GameType;

public class GameTypeService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextRepository _userContextRepository;

    public GameTypeService(IGameRepository gameRepository,
        IAuthorizationService authorizationService,
        IUserContextRepository userContextRepository)
    {
        _userContextRepository = userContextRepository;
        _authorizationService = authorizationService;
        _gameRepository = gameRepository;
    }

    public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
    {
        return await _gameRepository.GetGame(id);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> Search(string? name, int page,
        int pagesize)
    {
        return await _gameRepository.Search(name, page, pagesize);
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
        return await _gameRepository.ModifyGameType(id, gameRequest);
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

        return await _gameRepository.DeleteGame(id);
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(long? userId, GameRequest gameRequest)
    {
        return await _gameRepository.CreateGameType(userId, gameRequest);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGames(int page, int pagesize)
    {
        return await _gameRepository.GetGames(page, pagesize);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames(
        int page, int pagesize)
    {
        return await _gameRepository.GetAvailableGames(page, pagesize);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name)
    {
        return await _gameRepository.GetGamesByPlayer(name);
    }
}