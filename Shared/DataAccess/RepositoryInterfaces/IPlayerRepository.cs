using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPlayerRepository
{
    public Task<HandlerResult<Success, IErrorResult>> CreateAdminAsync(RegistrationRequest registrationRequest);
    public Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(RegistrationRequest registrationRequest);
    public Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest password, long? playerId);
    public Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest changeLoginRequest, long? playerId);
    public Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id);
    public Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id);
    public Task<HandlerResult<Success, IErrorResult>> SetPlayerLastLogin(string email, DateTime lastLogin);
    public Task<HandlerResult<SuccessData<PlayerInternalDto>, IErrorResult>> GetPlayerAsync(string email);
    public Task<HandlerResult<SuccessData<List<PlayerDto>>, IErrorResult>> GetPlayersAsync();
    public Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long ?playerId);
    
}