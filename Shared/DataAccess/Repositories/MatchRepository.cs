using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class MatchRepository
{
    private readonly DataContext _dataContext;

    public MatchRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> IsMatchPlayed(long tourId, string identifier)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Status == GameStatus.Played && x.Data.Equals(identifier));
        if(result == null) return new EntityNotFoundErrorResult();
        var bot = await _dataContext.Bots.FirstOrDefaultAsync(x => x.Id == result.Winner);
        if(bot == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Bot>()
        {
            Data = bot
        };
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> CreateMatch(long tourId, GameInfo gameInfo)
    {
        Matches match = new Matches();
        match.TournamentsId = tourId;
        var tour = await _dataContext.Tournaments.FindAsync(tourId);
        match.GameId = tour.GameId;
        match.Status = gameInfo.ReadyToPlay ? GameStatus.ReadyToPlay : GameStatus.NotReadyToPlay;
        match.Data = gameInfo.Key.ToString();
        match.MatchPlayers = new List<MatchPlayers>();
       

        Console.WriteLine("przed pentlą");
        var res = await _dataContext.Matches.AddAsync(match);
        //await _dataContext.SaveChangesAsync();
        foreach (var bot in gameInfo.Bots)
        {
            MatchPlayers players = new MatchPlayers();
            players.BotId = bot.Id;
            players.Matches = res.Entity;
            await _dataContext.MatchPlayers.AddAsync(players);
        }
        
        Console.WriteLine("po pentli");
        Console.WriteLine("pozas21");
        await _dataContext.SaveChangesAsync();
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateAllLadder(List<GameInfo> matches,long tourId)
    {
        foreach (var match in matches)
        {
            Matches m = new Matches();
            m.TournamentsId = tourId;
            var tour = await _dataContext.Tournaments.FindAsync(tourId);
            m.GameId = tour.GameId;
            m.Data = match.Key.ToString();
            m.Status = match.ReadyToPlay ? GameStatus.ReadyToPlay : GameStatus.NotReadyToPlay;
            var res = await _dataContext.Matches.AddAsync(m);
            foreach (var bot in match.Bots)
            {
                MatchPlayers players = new MatchPlayers();
                players.BotId = bot.Id;
                players.Matches = res.Entity;
                await _dataContext.MatchPlayers.AddAsync(players);
            }
        }
        await _dataContext.SaveChangesAsync();

        return new Success();
    }
    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> UpdateMatch(long tourId, GameInfo gameInfo,List<Bot> bots)
    {
        
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Data.Equals(gameInfo.Key.ToString()));
        Console.WriteLine("aaall "+ result);
        if (result == null) return new EntityNotFoundErrorResult();
        //await _dataContext.SaveChangesAsync();
        result.Status = gameInfo.ReadyToPlay ? GameStatus.ReadyToPlay : GameStatus.NotReadyToPlay;
        foreach (var bot in bots)
        {
            MatchPlayers players = new MatchPlayers();
            players.BotId = bot.Id;
            players.MatchId = result.Id;
            await _dataContext.MatchPlayers.AddAsync(players);
        }
        
        
        await _dataContext.SaveChangesAsync();
        return new SuccessData<long>()
        {
            Data = result.Id
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> Winner(long matchId, long winner)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);

        if (result == null) return new EntityNotFoundErrorResult();
        //await _dataContext.SaveChangesAsync();
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = winner;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<List<Bot>>, IErrorResult>> AllBots(long matchId)
    {
        var result = (await _dataContext.MatchPlayers.
            Where(x => x.MatchId == matchId).Include(x => x.Bot).
            ToListAsync()).ConvertAll(x=>x.Bot);
        
        return new SuccessData<List<Bot>>()
        {
            Data = result
        };
    }
    
    public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> GetGame(long matchId)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        var res2 = await _dataContext.Games.FindAsync(res.GameId);
        if (res2 == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>()
        {
            Data = res2
        };
    }
    
    
    public async Task<HandlerResult<SuccessData<Tournament>, IErrorResult>> GetTournament(long matchId)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        var res2 = await _dataContext.Tournaments.FindAsync(res.TournamentsId);
        if (res2 == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Tournament>()
        {
            Data = res2
        };
    }
    
}