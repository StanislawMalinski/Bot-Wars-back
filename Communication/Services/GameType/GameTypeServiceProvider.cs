using Shared.DataAccess.DTO;
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

    public async Task<HandlerResult<Success,IErrorResult>> deleteGameType(long id)
    {
        return await _gameRepository.DeleteGame(id);
    }

    public async Task<HandlerResult<Success,IErrorResult>> updateGameType(long id, GameDto game)
    {
        return await _gameRepository.ModifyGameType(id, game);
    }

    public async Task<HandlerResult<Success,IErrorResult>> addGameType(GameDto game)
    {
        return await _gameRepository.CreateGameType(game);
    }

    public async Task<HandlerResult<SuccessData<GameDto>,IErrorResult>> getGameType(long id)
    {
        return await _gameRepository.GetGameType(id);
    }

    public async Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>> getListOfTypesOfGames()
    {
        return await _gameRepository.GetGameTypes();
    }

    public async Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>> getListOfTypesOfAvailableGames()
    {
        return await _gameRepository.GetGameTypes();
    }
}