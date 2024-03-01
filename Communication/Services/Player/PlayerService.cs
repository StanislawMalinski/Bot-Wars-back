using Communication.ServiceInterfaces;
using Communication.Services.Validation;
using Shared.DataAccess.DTO;
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
    
    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.getPlayerInfo(PlayerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> registerNewPlayer(PlayerDto PlayerModel)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.registerNewPlayer(PlayerModel);
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.resetPassWordByLogin(Login);
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email)
    {
        _playerTypeService = Validate(login, key);
        return await _playerTypeService.resetPassWordByEmail(Email);
    }
}