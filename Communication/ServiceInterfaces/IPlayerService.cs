using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IPlayerService
{
    Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId);
    Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(RegistrationRequest registrationRequest);
    Task<HandlerResult<Success, IErrorResult>> RegisterNewAdmin(RegistrationRequest registrationRequest);
    Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login);
    Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email);
    Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest request, long? playerId);
    Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest request, long? playerId);
    Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto);
    Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long? playerId);
    Task<HandlerResult<SuccessData<List<GameSimpleResponse>>, IErrorResult>> GetGamesForPlayer(long playerId);
    Task<HandlerResult<Success, IErrorResult>> ChangePlayerImage(PlayerImageRequest imageRequest, long playerId);
    Task<HandlerResult<SuccessData<string>, IErrorResult>> GetPlayerImage(long playerId);
    Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id);
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetPlayerBots(long id);
}