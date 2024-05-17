using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IFileRepository _fileRepository;

    // move to config
    //private readonly IAuthorizationService _authorizationService;
    //private readonly IUserContextRepository _userContextRepository;

    public BotRepository(DataContext dataContext,
        IBotMapper botMapper,
        IFileRepository fileRepository
        //IAuthorizationService authorizationService,
        //IUserContextRepository userContextRepository
    )
    {
        //_userContextRepository = userContextRepository;
        //_authorizationService = authorizationService;
        _fileRepository = fileRepository;
        _dataContext = dataContext;
        _botMapper = botMapper;
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
        var res = await _fileRepository.GetFile(bot.FileId, bot.BotFile);
        return  res.Match(x=>x.Data, null!);
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
            long botFileId;
            var res = await _fileRepository.UploadFile(botRequest.BotFile);
            if (res.IsSuccess)
            {
                botFileId = res.Match(x => x.Data, x => 0);
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