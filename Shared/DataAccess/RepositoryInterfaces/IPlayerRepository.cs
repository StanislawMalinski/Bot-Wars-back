using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPlayerRepository
{
    Task<HandlerResult<Success, IErrorResult>> CreateAdminAsync(RegistrationRequest registrationRequest);
    Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(RegistrationRequest registrationRequest);
    Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest password, long? playerId);
    Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest changeLoginRequest, long? playerId);
    Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id);
    Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id);
    Task<HandlerResult<Success, IErrorResult>> SetPlayerLastLogin(string email, DateTime lastLogin);
    Task<HandlerResult<SuccessData<PlayerInternalDto>, IErrorResult>> GetPlayerAsync(string email);
    Task<HandlerResult<SuccessData<List<PlayerDto>>, IErrorResult>> GetPlayersAsync();
    Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long ?playerId);
    Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(string ?playerName);
    Task<HandlerResult<SuccessData<List<GameSimpleResponse>>, IErrorResult>> GetMyGames(long playerId);
    Task<HandlerResult<Success, IErrorResult>> ChangeImage(PlayerImageRequest imageRequest, long playerId);
    Task<HandlerResult<SuccessData<string>, IErrorResult>> GetImage( long playerId);
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId);


}