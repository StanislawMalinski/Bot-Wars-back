using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotRepository
{

    Task<bool> AddBot(BotRequest botRequest, long playerId);
    Task<bool> DeleteBot(long botId);
    Task SaveChangeAsync();

    Task<List<BotResponse>> GetPlayerBots(long playerId,
        PageParameters pageParameters);
    

    Task<Game?> GetGame(long botId);
    Task<Bot?> GetBot(long botId);
    

    Task<IFormFile?> GetBotFileForPlayer(long botId);

    Task<List<BotResponse>> GetBotsForTournament(long tournamentId,
        PageParameters pageParameters);


    Task<Bot?> GetBotAndCreator(long botId);
}