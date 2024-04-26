using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotService
{
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots();
    Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId);
    Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest,long playerId);
    Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId);
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId);
    Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>>GetBotFileForPlayer(long playerId, long botId);
    Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForTournament(long tournamentId);
}