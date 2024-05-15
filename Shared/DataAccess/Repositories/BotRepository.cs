using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories;

public class BotRepository : IBotRepository
{
    private readonly DataContext _dataContext;
    private readonly IBotMapper _botMapper;

    private readonly HttpClient _httpClient;

    // move to config
    private readonly string _gathererEndpoint = "http://host.docker.internal:7002/api/Gatherer";


    public BotRepository(DataContext dataContext,
        IBotMapper botMapper,
        HttpClient httpClient
    
    )
    {
      
        _dataContext = dataContext;
        _botMapper = botMapper;
        _httpClient = httpClient;
    }

    public async Task<Bot?> GetBotAndCreator(long botId)
    {
        return await _dataContext.Bots.Include(b => b.Player)
            .Where(b => b.Id == botId)
            .FirstOrDefaultAsync();
    }
    

    public async Task<List<BotResponse>> GetPlayerBots(long playerId,
        PageParameters pageParameters)
    {
        return await _dataContext
            .Bots
            .Where(bot => bot.PlayerId == playerId)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(bot => _botMapper.MapBotToResponse(bot))
            .ToListAsync();
    } 

    public async Task<IFormFile?> GetBotFileForPlayer(long botId)
    {
        var bot = await _dataContext
            .Bots
            .FirstOrDefaultAsync(bot => bot.Id == botId);

        if (bot == null) return null;

        try
        {
            HttpResponseMessage res = await _httpClient.GetAsync(string.Format(_gathererEndpoint, bot.FileId));
            if (!res.IsSuccessStatusCode) return null;
            
            Stream cont = await res.Content.ReadAsStreamAsync();

            IFormFile resBotFile = new FormFile(cont, 0, cont.Length, "botFile", bot.BotFile)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            return resBotFile;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BotResponse>> GetBotsForTournament(
        long tournamentId, PageParameters pageParameters)
    {
        
        return  await _dataContext.TournamentReferences
            .Where(tr => tr.Id == tournamentId)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Include(x => x.Bot)
            .Select(x=> _botMapper.MapBotToResponse(x.Bot))
            .ToListAsync();
        
    }

    public async Task<bool> AddBot(BotRequest botRequest, long playerId)

    {
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(botRequest.BotFile.OpenReadStream());
            content.Add(streamContent, "file", botRequest.BotFile.FileName);
            var res = await _httpClient.PutAsync(_gathererEndpoint, content);
            long botFileId;
            if (res.IsSuccessStatusCode)
            {
                string cont = await res.Content.ReadAsStringAsync();
                botFileId = Convert.ToInt32(cont);
            }
            else
            {
                return false;
            }

            var game = await _dataContext.Games.FindAsync(botRequest.GameId);
            if (game == null)
                return false;

            var bot = _botMapper.MapRequestToBot(botRequest);
            bot.FileId = botFileId;
            bot.PlayerId = playerId;
            await _dataContext.AddAsync(bot);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteBot(long botId)
    {
        var bot = await _dataContext.Bots.FindAsync(botId);
        if (bot == null) return false;
        _dataContext.Bots.Remove(bot);
        return true;
    }

   
    public async Task<Game?> GetGame(long botId)
    {
        return await _dataContext.Bots.Where(x => x.Id == botId)
            .Include(x => x.Games)
            .Select(x => x.Games).FirstAsync();
    }

    public async Task<Bot?> GetBot(long botId)
    {
        return await _dataContext.Bots.FindAsync(botId);
    }

    public async Task<EntityEntry<Bot>> AddBot(Bot bot)
    {
        return (await _dataContext.Bots.AddAsync(bot))!;
    }

    public async Task SaveChangeAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}