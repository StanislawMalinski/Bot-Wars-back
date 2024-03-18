using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType;

public class GameTypeServiceProvider
{
    private readonly IGameRepository _gameRepository;
    public GameTypeServiceProvider(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<HandlerResult<Success,IErrorResult>> DeleteGameType(long id)
    {
        return await _gameRepository.DeleteGame(id);
    }

    public async Task<HandlerResult<Success,IErrorResult>> UpdateGameType(long id, GameRequest game)
    {
        return await _gameRepository.ModifyGameType(id, game);
    }

    public async Task<HandlerResult<Success,IErrorResult>> AddGameType(GameRequest game)
    {
        return await _gameRepository.CreateGameType(game);
    }

    public async Task<HandlerResult<SuccessData<GameResponse>,IErrorResult>> GetGame(long id)
    {
        return await _gameRepository.GetGame(id);
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