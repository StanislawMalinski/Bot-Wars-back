using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotService
{
    Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId);
    Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest, long playerId);
    Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId);

    Task<HandlerResult<SuccessData<PageResponse<BotResponse>>, IErrorResult>> GetBotsForPlayer(string? playerName,
        PageParameters pageParameters);

    Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetBotFileForPlayer(long playerId, long botId);

    Task<HandlerResult<SuccessData<PageResponse<BotResponse>>, IErrorResult>> GetBotsForTournament(long tournamentId,
        PageParameters pageParameters);
}