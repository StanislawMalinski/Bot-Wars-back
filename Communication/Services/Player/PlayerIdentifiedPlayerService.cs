using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerIdentifiedPlayerService : PlayerUnidentifiedPlayerService , IPlayerService
{
    public PlayerIdentifiedPlayerService(PlayerServiceProvider playerServiceProvider) : base(playerServiceProvider)
    {
        
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        return  await _playerServiceProvider.getPlayerInfo(PlayerId);
    }
}