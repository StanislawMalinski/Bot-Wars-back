using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services;

public class GameTypeServiceProvider
{
    private IGameService _gameService;
    public GameTypeServiceProvider(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async void deleteGameType(Game game)
    {
        _gameService.DeleteGameAsync(game.Id);
    }

    public async void updateGameType(Game game)
    {
        _gameService.UpdateGameAsync(game);
    }

    public async void addGameType(Game game)
    {
        
    }

    public async void getGameType(long id)
    {
        var result = await _gameService.GetGameAsync(id);
        
    }

    public async void getListOfTypesOfGames()
    {
        var result = await _gameService.GetGamesAsync();
    }

    public async void getListOfTypesOfAvailableGames()
    {
        var result = await _gameService.GetGamesAsync();
        

    }
}