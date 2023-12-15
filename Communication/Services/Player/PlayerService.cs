using Communication.ServiceInterfaces;
using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerService : Service<IPlayerService>
{

    private IPlayerService? _playerTypeService;
    private string login = "login"; // should be obtained in method call
    private string key = "key";
    
    public PlayerService(PlayerAdminService adminService,
        PlayerBadValidation badValidation,
        PlayerBannedPlayerService bannedPlayerService,
        PlayerIdentifiedPlayerService identifiedPlayerService,
        PlayerUnidentifiedPlayerService unidentifiedPlayerService,
        IPlayerValidator validator)
        : base(adminService, badValidation, bannedPlayerService, identifiedPlayerService, unidentifiedPlayerService,
            validator)
    {
        
    }
    
    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerInfo(long PlayerId)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.GetPlayerInfo(PlayerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(PlayerDto PlayerModel)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.RegisterNewPlayer(PlayerModel);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ResetPassWordByLogin(string Login)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.ResetPassWordByLogin(Login);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ResetPassWordByEmail(string Email)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.ResetPassWordByEmail(Email);
    }
}