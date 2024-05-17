using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;


namespace Shared.DataAccess.Repositories;

public class MatchRepository
{
    private readonly DataContext _dataContext;
    private readonly MatchMapper _mapper;

    public MatchRepository(DataContext dataContext, MatchMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
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
        Console.WriteLine(res == null);
        if (res == null) return false;
        return true;
    }

    public async Task<Matches> GetMatchByTourIdEndKey(long tournamentId,string parsPkey)
    {
        return await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tournamentId && x.Data.Equals(parsPkey));
    }
 
    public async Task<List<long>> GetAllReadyToPlay(long tourId)
    {
        return await _dataContext.Matches.Where(x => x.TournamentsId == tourId && x.Status == GameStatus.ReadyToPlay)
            .Select(x => x.Id).ToListAsync();
    }
    public async Task<List<long>> GetAllPlayed(long tourId)
    {
        return await _dataContext.Matches.Where(x => x.TournamentsId == tourId && x.Status==GameStatus.Played).Select(x => x.Id).ToListAsync();
    }
    public async Task<Matches?> IsResolve(long tourId,string key)
    {
        return await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Data.Equals(key) && x.Status == GameStatus.Resolve);
       
    }

    public async Task<List<long>> GetAllLosers(long matchId,long winner)
    {
         return await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId && x.BotId != winner).Include(x=>x.Bot).Select(x=>x.Bot.PlayerId).ToListAsync();
    }

    public async Task<Bot?> GetPlayerFromBot(long botId)
    {
        return await _dataContext.Bots.FindAsync(botId);
    }

    public IQueryable<Matches> GetListOfUnfilteredMatches()
    {
        return _dataContext.Matches
            .Include(matches => matches.MatchPlayers)
            .Include(matches => matches.Tournament)
            .Include(matches => matches.Game)
            .Where(match => true);
    }
    
    public async Task<Matches?> GetMatchById(long matchId)
    {
        return await _dataContext.Matches.Where(matches =>  matches.Id == matchId).Include(x=>x.MatchPlayers)
            .Include(matches => matches.Game).FirstOrDefaultAsync();
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

    public async Task<List<MatchInTournamentRespond>> GetTournamentStatus(long tourId)
    {
        return await _dataContext.Matches.Where(x => x.TournamentsId == tourId)
            .Include(x => x.MatchPlayers).Select(x => _mapper.MapEntityToMatcInTournamentResponse(x)).ToListAsync();
    }
 }