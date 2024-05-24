using Engine.Services;
using Shared.DataAccess.Context;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class AchievementHandlerService
{
  
    private readonly MatchRepository _matchRepository;
    private readonly PointsEngineAccessor _pointsEngineAccessor;
    private readonly TournamentRepository _tournamentRepository;
    private readonly IAchievementsRepository _achievementsRepository;
    private readonly TaskService _taskService;

    public AchievementHandlerService(MatchRepository matchRepository, PointsEngineAccessor pointsEngineAccessor, TournamentRepository tournamentRepository, IAchievementsRepository achievementsRepository, TaskService taskService)
    {
        _matchRepository = matchRepository;
        _pointsEngineAccessor = pointsEngineAccessor;
        _tournamentRepository = tournamentRepository;
        _achievementsRepository = achievementsRepository;
        _taskService = taskService;
    }

    public async  Task<HandlerResult<Success,IErrorResult>> MatchWinner(long matchId, long winner, long taskId,long logId, MatchResult matchResult)
    {
        Console.WriteLine($"{matchId} zwyciesca  meczu");
        var losers = await _matchRepository.GetAllLosers(matchId, winner);
        var playerWinner = ((await _matchRepository.GetPlayerFromBot(winner))!).Id;
        var tour = await _matchRepository.GetTournament(matchId);
        foreach (var loser in losers!)
        {
            await _pointsEngineAccessor.MatchCalculation(playerWinner, loser,tour!.Id);
        }
        //return await _matchRepository.Winner( matchId, winner, taskId);

        var result = await _matchRepository.GetMatchById(matchId);
        var taskDone = (await _taskService.GetTask(taskId)).Match(x=>x.Data,null);
        if (result == null || taskDone == null) return new EntityNotFoundErrorResult();
        taskDone.Status  = Shared.DataAccess.Enumerations.TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.LogId = logId;
        result.Winner = winner;
        result.MatchResult = matchResult;
        Console.WriteLine($"{matchId}  pobrnaie przliczeni ");
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.WinGames, winner);
        var bots = await _matchRepository.GetMatchBots(matchId);
        foreach (var bot in bots)
        {
            await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, bot.Id);
        }
        Console.WriteLine($"{matchId}  po przelcizneiu ");
        await _matchRepository.SaveChangesAsync();
        Console.WriteLine($"{matchId}  zapisanie");
        return new Success();
    }
        
    
    
    public async Task<HandlerResult<Success,IErrorResult>> EndTournament(long tourId,long botId, long taskId)
    {

        var res = await _tournamentRepository.GetTournament(tourId);
        var taskRes = (await _taskService.GetTask(taskId)).Match(x=>x.Data,null!);
        if (res == null || taskRes == null) return new EntityNotFoundErrorResult();
        res.Status = TournamentStatus.PLAYED;
        taskRes.Status = Shared.DataAccess.Enumerations.TaskStatus.Done;
        await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.TournamentsWon, botId);
        await _tournamentRepository.SaveChangesAsync();
        return new Success();
    }
    public async Task<HandlerResult<Success, IErrorResult>> UpDateAchievement(AchievementsTypes achievementsTypes, long botId)
    {
        var res = await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.GamePlayed, botId);
        if (res) return new Success();
        return new IncorrectOperation();
    }
   
}