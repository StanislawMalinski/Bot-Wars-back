using Communication.Services.Validation;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Bot;

public class BotService : Service<IBotService>
{
    private IBotService _botService;
    private string login = "login";
    private string key = "key";

    public BotService(
        BotAdminService adminInterface,
        BotIdentifiedPlayerService identifiedPlayerInterface,
        BotUnidentifiedPlayerService unidentifiedPlayerInterface,
        BotBannedPlayerService bannedPlayerInterface,
        BotBadValidationService badValidationInterface,
        IPlayerValidator validator
    ) : base(
        adminInterface,
        identifiedPlayerInterface,
        bannedPlayerInterface,
        unidentifiedPlayerInterface,
        badValidationInterface,
        validator
    )
    {
    }
    
    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots()
    {
        _botService = Validate(login, key);
        return await _botService.GetAllBots();
    }

    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        _botService = Validate(login, key);
        return await _botService.GetBotResponse(botId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        _botService = Validate(login, key);
        return await _botService.AddBot(botRequest);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        _botService = Validate(login, key);
        return await _botService.DeleteBot(botId);
    }
}