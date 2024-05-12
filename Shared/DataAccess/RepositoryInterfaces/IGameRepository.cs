﻿using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IGameRepository
{
    public Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name, PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> CreateGameType(long? userId,GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGames(PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id);
    public Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id);
    public Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetAvailableGames(PageParameters pageParameters);
    public Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> Search(string? name, PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> GameNotAvailableForPlay(long gameId);
    public Task<long?> GetCreatorId(long gameId);

}