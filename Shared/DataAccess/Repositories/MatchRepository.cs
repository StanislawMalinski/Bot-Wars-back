using Microsoft.EntityFrameworkCore;
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
    private readonly IAchievementsRepository _achievementsRepository;

    public MatchRepository(DataContext dataContext,IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
        _dataContext = dataContext;
    }

    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> IsMatchPlayed(long tourId, string identifier)
    {
        Console.WriteLine(tourId +" " + identifier);
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Status == GameStatus.Played && x.Data.Equals(identifier) && x.Winner > 0);
        
        if(result == null) return new EntityNotFoundErrorResult();
        
        Console.WriteLine(result.Winner+" "+result.Id+" "+ result.Status);
        var bot = await _dataContext.Bots.FirstOrDefaultAsync(x => x.Id == result.Winner);
        if(bot == null) return new EntityNotFoundErrorResult();
        Console.WriteLine("asdasd12345");
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
       
        
        var res = await _dataContext.Matches.AddAsync(match);
        //await _dataContext.SaveChangesAsync();
        foreach (var bot in gameInfo.Bots)
        {
            MatchPlayers players = new MatchPlayers();
            players.BotId = bot.Id;
            players.Matches = res.Entity;
            await _dataContext.MatchPlayers.AddAsync(players);
        }
        
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
            m.Winner = -1;
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
    public async Task<HandlerResult<Success, IErrorResult>> PlayMatch(long tourId, string identifier)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId  && x.Data.Equals(identifier));
        Console.WriteLine("no czy jes");
        if(result == null) return new EntityNotFoundErrorResult();
        Console.WriteLine("jest");
        if (result.Status == GameStatus.Playing || result.Status == GameStatus.Played)
        {
            return new Success();
        }
        result.Status = GameStatus.Playing;
        _Task task = new _Task();
        task.OperatingOn = result.Id;
        task.ScheduledOn = DateTime.Now;
        task.Status = TaskStatus.Unassigned;
        task.Type = TaskTypes.PlayGame;
        await _dataContext.Tasks.AddAsync(task);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    public async Task<HandlerResult<Success, IErrorResult>> PlayMatch(long matchId)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        if(result == null) return new EntityNotFoundErrorResult();
        if (result.Status == GameStatus.Playing || result.Status == GameStatus.Played) return new Success();
        if (result.Status == GameStatus.NotReadyToPlay) return new IncorrectOperation();
        result.Status = GameStatus.Playing;
        _Task task = new _Task();
        task.OperatingOn = result.Id;
        task.ScheduledOn = DateTime.Now;
        task.Status = TaskStatus.Unassigned;
        task.Type = TaskTypes.PlayGame;
        await _dataContext.Tasks.AddAsync(task);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
    
    public async Task<HandlerResult<Success, IErrorResult>> Winner(long matchId, long winner, long taskId)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        var taskDone = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result == null || taskDone == null) return new EntityNotFoundErrorResult();
        //await _dataContext.SaveChangesAsync();
        taskDone.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = winner;
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, winner);
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.WinGames, winner);
        var bots = await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId && x.BotId != winner).ToListAsync();
        foreach (var bot in bots)
        {
            await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, bot.Id);
        }
        
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> GameFiled(long matchId, long taskId)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        var taskDone = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result == null || taskDone == null) return new EntityNotFoundErrorResult();
        //await _dataContext.SaveChangesAsync();
        var random = await _dataContext.MatchPlayers.FirstOrDefaultAsync(x => x.MatchId == matchId);
        taskDone.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = random!.Id;
        
        
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> Loser(long matchId, long loser, long taskId)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId);
        var taskDone = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result == null || taskDone == null) return new EntityNotFoundErrorResult();
        var winner = await _dataContext.MatchPlayers.FirstOrDefaultAsync(x => x.MatchId == matchId && x.BotId != loser);
        //await _dataContext.SaveChangesAsync();
        taskDone.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = winner.Id;
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, winner.Id);
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.WinGames, winner.Id);
        var bots = await _dataContext.MatchPlayers.Where(x => x.MatchId == matchId && x.BotId != winner.Id).ToListAsync();
        foreach (var bot in bots)
        {
            await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, bot.Id);
        }
        
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
    
    public async Task<HandlerResult<Success, IErrorResult>> AreAny(long tourId)
    {
        var res = await _dataContext.Matches.FirstOrDefaultAsync(x => x.TournamentsId == tourId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<List<GameInfo>>, IErrorResult>> RestoreLather(long tourId)
    {
        var res = await _dataContext.Matches.Where(x => x.TournamentsId == tourId)
            .Select(x => new GameInfo(
                x.Status == GameStatus.Played,
                Int32.Parse(x.Data),
                null,
                 _dataContext.MatchPlayers.Where(a => a.MatchId == x.Id).Include(a => a.Bot).Select(a=>a.Bot).ToList()
                ))
            .ToListAsync();
        return new SuccessData<List<GameInfo>>()
        {
            Data = res
        };
        
    }

    public async Task<HandlerResult<Success, IErrorResult>> ResolveMatch(long matchId)
    {
        var match = await _dataContext.Matches.FindAsync(matchId);
        if (match == null) return new EntityNotFoundErrorResult();
        if (match.Status != GameStatus.Played) return new IncorrectOperation();
        int key = Int32.Parse(match.Data);
        match.Status = GameStatus.Resolve;
        if (key == 0)
        {
            await _dataContext.SaveChangesAsync();
            return new Success();
        }
        int pkey = ((key - 1) / 2);
        string parsPkey = pkey.ToString();
        var childMatch =
            await _dataContext.Matches.FirstOrDefaultAsync(x =>
                x.TournamentsId == match.TournamentsId && x.Data.Equals(parsPkey));
        if (childMatch == null)
        {
            childMatch = new Matches();
            childMatch.Data = parsPkey;
            childMatch.GameId = match.GameId;
            childMatch.TournamentsId = match.TournamentsId;
            childMatch.Winner = -1;
            childMatch.Status = GameStatus.NotReadyToPlay;
            await _dataContext.Matches.AddAsync(childMatch);
        }
        else
        {
            childMatch.Status = GameStatus.ReadyToPlay;
        }

        MatchPlayers newBot = new MatchPlayers();
        newBot.BotId = match.Winner;
        newBot.Matches = childMatch;
        await _dataContext.MatchPlayers.AddAsync(newBot);
        await _dataContext.SaveChangesAsync();
        return new Success();

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
    
    
  
    
}