using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType;

public class GameTypeServiceProvider
{
    private readonly IGameRepository _gameRepository;
    public GameTypeServiceProvider(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<ServiceResponse<GameDto>> deleteGameType(long id)
    {
        return await _gameRepository.DeleteGame(id);
    }

    public async Task<ServiceResponse<GameDto>> updateGameType(long id, GameDto game)
    {
        return await _gameRepository.ModifyGameType(id, game);
    }

    public async Task<ServiceResponse<GameDto>> addGameType(GameDto game)
    {
        return await _gameRepository.CreateGameType(game);
    }

    public async Task<ServiceResponse<GameDto>> getGameType(long id)
    {
        return await _gameRepository.GetGameType(id);
    }

    public async Task<ServiceResponse<List<GameDto>>> getListOfTypesOfGames()
    {
        return await _gameRepository.GetGameTypes();
    }

    public async Task<ServiceResponse<List<GameDto>>> getListOfTypesOfAvailableGames()
    {
        return await _gameRepository.GetGameTypes();
    }
}