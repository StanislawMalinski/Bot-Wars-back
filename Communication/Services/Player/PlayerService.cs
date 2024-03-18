using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        return await _playerRepository.GetPlayerAsync(PlayerId);
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> registerNewPlayer(PlayerDto PlayerModel)
    {
        return await _playerRepository.CreatePlayerAsync(PlayerModel);
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest request, long userId)
    {
        return await _playerRepository.ChangePassword(request, userId);
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto)
    {
        return await _playerRepository.GenerateJwt(dto);
    }

    public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long playerId)
    {
        throw new NotImplementedException();
    }
}