using Engine.Services;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class TournamentResolver : Resolver
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly TaskService _taskService;
    private readonly MatchRepository _matchRepository;
    private readonly AchievementHandlerService _achievementHandlerService;

    public TournamentResolver(TournamentRepository tournamentRepository, TaskService taskService, MatchRepository matchRepository, AchievementHandlerService achievementHandlerService)
    {
        _tournamentRepository = tournamentRepository;
        _taskService = taskService;
        _matchRepository = matchRepository;
        _achievementHandlerService = achievementHandlerService;
    }
    
    

    public async Task<HandlerResult<Success,IErrorResult>> EndTournament(long tournamentId, long taskId)
    {

        var res = await _tournamentRepository.GetTournament(tournamentId);
        var taskRes = await _taskService.GetTask(taskId);
        if (res == null || taskRes.IsError) return new EntityNotFoundErrorResult();
        var task = taskRes.Match(x => x.Data, null!);
        res.Status = TournamentStatus.PLAYED;
        task!.Status = TaskStatus.Done;
        await _tournamentRepository.SaveChangesAsync();
        return new Success();
        
        
    }
    
    public async Task<HandlerResult<Success,IErrorResult>> EndTournament(long tourId,long botId, long taskId)
    {
        return await _achievementHandlerService.EndTournament(tourId, botId, taskId);
    }
    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await  _taskService.GetTask(taskId);
    }

    public async  Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetGame(long tourId)
    {
        var res = await _tournamentRepository.TournamentGame(tourId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>()
        {
            Data = res
        };
    }

    public async Task<HandlerResult<Success,IErrorResult>> AreAnyMatchesPlayed(long tourId)
    {
        if (await _matchRepository.AreAny(tourId)) return new Success();
        Console.WriteLine("hejo2");
        return new EntityNotFoundErrorResult();
    }

    public async Task<HandlerResult<Success,IErrorResult>> StartPlaying(long tourId)
    {
        if (await _tournamentRepository.TournamentPlaying(tourId)) return new Success();
        return new IncorrectOperation();
    }
    
    
    public async Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetRegisterBots (long tourId)
    {
        return new SuccessData<List<Bot>>()
        {
            Data = await _tournamentRepository.TournamentBotsToPlay(tourId)
        };

    }
    
    public async Task<HandlerResult<Success,IErrorResult>> CreateLadder(List<GameInfo> matches, long tourId)
    {
        
            foreach (var match in matches)
            {
                Matches m = new Matches();
                m.TournamentsId = tourId;
                var tour = await _tournamentRepository.GetTournament(tourId);
                m.GameId = tour.GameId;
                m.Data = match.Key.ToString();
                m.Winner = -1;
                m.Status = match.ReadyToPlay ? GameStatus.ReadyToPlay : GameStatus.NotReadyToPlay;
                var res = await _matchRepository.AddMatch(m);
                foreach (var bot in match.Bots)
                {
                    MatchPlayers players = new MatchPlayers();
                    players.BotId = bot.Id;
                    players.Matches = res.Entity;
                    await _matchRepository.AddMatch(players);
                }
            }
            await _matchRepository.SaveChangesAsync();
            return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<List<long>>,IErrorResult>> GetMatchesReadyToPlay(long tourId)
    {
        return new SuccessData<List<long>>()
        {
            Data = await _matchRepository.GetAllReadyToPlay(tourId)
        };
    }
   
    public async Task<HandlerResult<Success,IErrorResult>> PlayMatch(long matchId)
    {
        Console.WriteLine($"{matchId} rozegranie gry oepracje");
        var result = await _matchRepository.GetMatch(matchId);
        if(result == null) return new EntityNotFoundErrorResult();
        if (result.Status == GameStatus.Playing || result.Status == GameStatus.Played) return new Success();
        if (result.Status == GameStatus.NotReadyToPlay) return new IncorrectOperation();
        result.Status = GameStatus.Playing;
        _Task task = new _Task();
        task.OperatingOn = result.Id;
        task.ScheduledOn = DateTime.Now;
        task.Status = TaskStatus.Unassigned;
        task.Type = TaskTypes.PlayGame;
        Console.WriteLine($"{matchId} regane");
        await _taskService.CreateTask(task);
        await _matchRepository.SaveChangesAsync();
        Console.WriteLine($"{matchId} zapisnaie");
        return new Success();
        
    }
    
    public async Task<HandlerResult<SuccessData<List<long>>,IErrorResult>> GetPlayedMatches(long tourId)
    {
        return new SuccessData<List<long>>() { Data = await _matchRepository.GetAllPlayed(tourId) };
    }
    
    public async Task<HandlerResult<Success,IErrorResult>> ResolveMatch(long matchId)
    {
        Console.WriteLine($"{matchId} resoveld macth ");
        var match = await _matchRepository.GetMatch(matchId);
        if (match == null) return new EntityNotFoundErrorResult();
        if (match.Status != GameStatus.Played) return new IncorrectOperation();
        int key = Int32.Parse(match.Data);
        match.Status = GameStatus.Resolve;
        Console.WriteLine($"{matchId} pobranie ");
        if (key == 0)
        {
            await _matchRepository.SaveChangesAsync();
            return new Success();
        }
        int pkey = ((key - 1) / 2);
        string parsPkey = pkey.ToString();
        var childMatch = await _matchRepository.GetMatchByTourIdEndKey(match.TournamentsId, parsPkey);
        if (childMatch == null)
        {
            childMatch = new Matches();
            childMatch.Data = parsPkey;
            childMatch.GameId = match.GameId;
            childMatch.TournamentsId = match.TournamentsId;
            childMatch.Winner = -1;
            childMatch.Status = GameStatus.NotReadyToPlay;
            await _matchRepository.AddMatch(childMatch);
        }
        else
        {
            childMatch.Status = GameStatus.ReadyToPlay;
        }

        MatchPlayers newBot = new MatchPlayers();
        newBot.BotId = match.Winner;
        newBot.Matches = childMatch;
        Console.WriteLine($"{matchId} jest wynik");
        await _matchRepository.AddMatch(newBot);
        await _matchRepository.SaveChangesAsync();
        Console.WriteLine($"{matchId} zapisanie gwynik gry");
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<long>,IErrorResult>> IsMatchResolved(long tourId,string key)
    {
        
        var res = await _matchRepository.IsResolve(tourId,key);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<long>()
        {
            Data = res.Winner
        };
    }
    
}