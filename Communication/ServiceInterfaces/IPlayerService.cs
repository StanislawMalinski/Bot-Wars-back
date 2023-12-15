using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IPlayerService
{
    public Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerInfo(long PlayerId);
    public Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(PlayerDto PlayerModel);
    public Task<HandlerResult<Success, IErrorResult>> ResetPassWordByLogin(string Login);
    public Task<HandlerResult<Success, IErrorResult>> ResetPassWordByEmail(string Email);
    
   
}