using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories;

public class MatchRepository
{
    private readonly DataContext _dataContext;

    public MatchRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Matches?> GetMatch(long matchId)
    {
        return await _dataContext.Matches.FindAsync(matchId);
    }
    public async Task<MatchPlayers?> GetMatchPlayerByMatchId(long matchId)
    {
        return await _dataContext.MatchPlayers.FirstOrDefaultAsync(x => x.MatchId == matchId);
    }
    
    public async Task<EntityEntry<Matches>> AddMatch(Matches matches)
    {
        return await _dataContext.Matches.AddAsync(matches);
    }

    public async Task<EntityEntry<MatchPlayers>> AddMatch(MatchPlayers matchPlayers)
    {
        return await _dataContext.MatchPlayers.AddAsync(matchPlayers);
    }

    public async Task<MatchPlayers?> GetMatchPlayerByIdNoBotId(long matchId, long botId)
    {
        return await  _dataContext.MatchPlayers.FirstOrDefaultAsync(x => x.MatchId == matchId && x.BotId != botId);
    }

    public async Task<List<MatchPlayers>> GetListMatchPlayersByMatchIdAndNoBotId(long matchId,long botId)
    {
        return await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId && x.BotId != botId).ToListAsync();
    }
    
    public async Task<List<Bot>> AllBots(long matchId)
    {
        return (await _dataContext.MatchPlayers.
            Where(x => x.MatchId == matchId).Include(x => x.Bot).
            ToListAsync()).ConvertAll(x=>x.Bot)!;
    }
    
    public async Task<Game?> GetGame(long matchId)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        if (res == null) return null;
        return await _dataContext.Games.FindAsync(res.GameId);
    }
    
    
    public async Task<bool> AreAny(long tourId)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x => x.TournamentsId == tourId);
        if (res == null) return false;
        return true;
    }

    public async Task<Matches> GetMatchByTourIdEndKey(long tournamentId,string parsPkey)
    {
        return await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tournamentId && x.Data.Equals(parsPkey));
    }
 
    public async Task<HandlerResult<SuccessData<List<long>>, IErrorResult>> GetAllReadyToPlay(long tourId)
    {
        return new SuccessData<List<long>>()
        {
            Data = await _dataContext.Matches.Where(x => x.TournamentsId == tourId && x.Status == GameStatus.ReadyToPlay).Select(x => x.Id).ToListAsync()
        };
    }
    public async Task<HandlerResult<SuccessData<List<long>>, IErrorResult>> GetAllPlayed(long tourId)
    {
        return new SuccessData<List<long>>()
        {
            Data = await _dataContext.Matches.Where(x => x.TournamentsId == tourId && x.Status==GameStatus.Played).Select(x => x.Id).ToListAsync()
        };
    }
    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> IsResolve(long tourId,string key)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Data.Equals(key) && x.Status == GameStatus.Resolve);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<long>()
        {
            Data = res.Winner
        };
    }

    public async Task<HandlerResult<SuccessData<List<long>>, IErrorResult>> GetAllLosers(long matchId,long winner)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
     
        if (result == null) return new EntityNotFoundErrorResult();
        var losers = await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId && x.BotId != winner).Include(x=>x.Bot).Select(x=>x.Bot.PlayerId).ToListAsync();
        return new SuccessData<List<long>>()
        {
            Data = losers
        };
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPlayerFromBot(long botId)
    {
        var result = await _dataContext.Bots.FindAsync(botId);
        if(result == null) return new EntityNotFoundErrorResult();
        return new SuccessData<long>()
        {
            Data = result.PlayerId
        };
    }


    public IQueryable<Matches> GetListOfUnfilteredMatches()
    {
        return _dataContext.Matches
            .Include(matches => matches.MatchPlayers)
            .Include(matches => matches.Tournament)
            .Include(matches => matches.Game)
            .Where(match => true);
    }
    
    public async Task<Matches?> GetMatchById(long id)
    {
        return await _dataContext.Matches.Include(matches => matches.Tournament)
            .Include(matches => matches.Game).Where(match => match.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Bot?>> GetMatchBots(long matchId)
    {
        return await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId ).Include(x=>x.Bot).Select(x=>x.Bot).ToListAsync();
    }
    public async Task<List<long>> GetAllMatchesByTourId(long tourId)
    {
        return await _dataContext.Matches.Where(x => x.TournamentsId == tourId).Select(x=>x.Id).ToListAsync();
    }

    public async Task<List<long>> GetAllMatchesByTourId(long tourId, GameStatus status)
    {
        return await _dataContext.Matches.Where(x => x.TournamentsId == tourId && x.Status == status)
            .Select(x => x.Id).ToListAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task<Tournament?> GetTournament(long matchId)
    {
        return await _dataContext.Matches.Where (x => x.Id == matchId).Include (x => x.Tournament).Select (x=>x.Tournament).FirstAsync();
    }
 }