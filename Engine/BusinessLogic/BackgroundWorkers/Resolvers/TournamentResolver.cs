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
    
    

    public async Task<HandlerResult<Success,IErrorResult>> EndTournament(long tourId,long taskId)
    {
        return await _tournamentRepository.TournamentEnded(tourId,taskId);
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
        return await _tournamentRepository.TournamentGame(tourId);
    }

    public async Task<HandlerResult<Success,IErrorResult>> AreAnyMatchesPlayed(long tourId)
    {
        if (await _matchRepository.AreAny(tourId)) return new Success();
        return new EntityNotFoundErrorResult();
    }

    public async Task<HandlerResult<Success,IErrorResult>> StartPlaying(long tourId)
    {
        return await _tournamentRepository.TournamentPlaying(tourId);
    }
    
    
    public async Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetRegisterBots (long tourId)
    {
        return await _tournamentRepository.TournamentBotsToPlay(tourId);
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
        return await _matchRepository.GetAllReadyToPlay(tourId);
    }
   
    public async Task<HandlerResult<Success,IErrorResult>> PlayMatch(long matchId)
    {

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
        await _taskService.CreateTask(task);
        await _taskService.CreateTask(task);
        return new Success();
        
    }
    
    public async Task<HandlerResult<SuccessData<List<long>>,IErrorResult>> GetPlayedMatches(long tourId)
    {
        return await _matchRepository.GetAllPlayed(tourId);
    }
    
    public async Task<HandlerResult<Success,IErrorResult>> ResolveMatch(long matchId)
    {
        var match = await _matchRepository.GetMatch(matchId);
        if (match == null) return new EntityNotFoundErrorResult();
        if (match.Status != GameStatus.Played) return new IncorrectOperation();
        int key = Int32.Parse(match.Data);
        match.Status = GameStatus.Resolve;
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
        await _matchRepository.AddMatch(newBot);
        await _matchRepository.SaveChangesAsync();
        return new Success();
    }
    
    public async Task<HandlerResult<SuccessData<long>,IErrorResult>> IsMatchResolved(long tourId,string key)
    {
        return await _matchRepository.IsResolve(tourId,key);
    }
    
}