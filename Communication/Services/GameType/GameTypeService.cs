using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType;

public class GameTypeService : IGameService
{
    private readonly IGameRepository _gameRepository;
    public GameTypeService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<HandlerResult<SuccessData<GameResponse>,IErrorResult>> GetGame(long id)
    {
        return await _gameRepository.GetGame(id);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> Search(string? name)
    {
        return await _gameRepository.Search(name);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest)
    {
        return await _gameRepository.ModifyGameType(id, gameRequest);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        return await _gameRepository.DeleteGame(id);
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(long userId,GameRequest gameRequest)
    {
        return await _gameRepository.CreateGameType(userId,gameRequest);
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetGames()
    {
        return await _gameRepository.GetGames();
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetListOfTypesOfAvailableGames()
    {
        return await _gameRepository.GetAvailableGames();
    }
}