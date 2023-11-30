using Shared.DataAccess.DAO;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services;

public class GameTypeServiceProvider
{
    private readonly IGameRepository _gameRepository;
    private GameTypeMapper _mapper;
    public GameTypeServiceProvider(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _mapper = new GameTypeMapper();
    }

    public async void deleteGameType(long id)
    {
        _gameRepository.DeleteGame(id);
    }

    public async void updateGameType(long id, GameDto game)
    {

        _gameRepository.ModifyGameType(id, _mapper.ToGameType(game));
    }

    public async void addGameType(GameDto game)
    {
        _gameRepository.CreateGameType(_mapper.ToGameType(game));
    }

    public async Task<GameDto> getGameType(long id)
    {
        var result = await _gameRepository.GetGameType(id);
        if (result.Success)
        {
            return _mapper.ToDto(result.Data);
        }

        return null;
    }

    public async Task<List<GameDto>> getListOfTypesOfGames()
    {
        var result = await _gameRepository.GetGameTypes();
        if (result.Success)
        {
            return result.Data.Select(x => _mapper.ToDto(x)).ToList();
            
        }

        return null;
    }

    public async Task<List<GameDto>> getListOfTypesOfAvailableGames()
    {
        var result = await _gameRepository.GetGameTypes();
        if (result.Success)
        {
            return result.Data.Where(x => x.IsAvaiableForPlay == true)
                .Select(x => _mapper.ToDto(x)).ToList();
            
        }

        return null;
    }
}