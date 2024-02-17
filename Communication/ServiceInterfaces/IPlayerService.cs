using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IPlayerService
{
    public Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId);
    public Task<HandlerResult<Success, IErrorResult>> registerNewPlayer(PlayerDto PlayerModel);
    public Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login);
    public Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email);
    public Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto);
}