using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeService : IAdministrativeService
{
    private readonly IPlayerRepository _playerRepository;
    private string login = "login";
    private string key = "key";

    public AdministrativeService( IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        var resPlayer =  await _playerRepository.GetPlayer(playerId);
        if (resPlayer == null) return new EntityNotFoundErrorResult();
        resPlayer.isBanned = false;
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        var resPlayer =  await _playerRepository.GetPlayer(playerId);
        if (resPlayer == null) return new EntityNotFoundErrorResult();
        resPlayer.isBanned = true;
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }
}