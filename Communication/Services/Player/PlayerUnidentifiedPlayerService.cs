using Communication.ServiceInterfaces;
using Communication.Services.Tournament;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerUnidentifiedPlayerService : IPlayerService
{
    protected PlayerServiceProvider _playerServiceProvider;

    public PlayerUnidentifiedPlayerService(PlayerServiceProvider playerServiceProvider )
    {
        _playerServiceProvider = playerServiceProvider;
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerInfo(long PlayerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(PlayerDto PlayerModel)
    {
        return await RegisterNewPlayer(PlayerModel);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ResetPassWordByLogin(string Login)
    {
        return await _playerServiceProvider.resetPassWordByLogin(Login);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ResetPassWordByEmail(string Email)
    {
        return await _playerServiceProvider.resetPassWordByEmail(Email);
    }
    

}