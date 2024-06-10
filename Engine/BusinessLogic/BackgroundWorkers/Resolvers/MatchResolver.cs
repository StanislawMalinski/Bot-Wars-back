using Engine.FileWorker;
using Engine.Services;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class MatchResolver : Resolver
{
    private readonly AchievementHandlerService _achievementHandlerService;
    private readonly FileManager _fileManager;
    private readonly IGameRepository _gameRepository;
    private readonly MatchRepository _matchRepository;
    private readonly TaskService _taskService;

    public MatchResolver(TaskService taskService, MatchRepository matchRepository,
        AchievementHandlerService achievementHandlerService, IGameRepository gameRepository, FileManager fileManager)
    {
        _taskService = taskService;
        _matchRepository = matchRepository;
        _achievementHandlerService = achievementHandlerService;
        _gameRepository = gameRepository;
        _fileManager = fileManager;
    }


    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await _taskService.GetTask(taskId);
    }

    public async Task<HandlerResult<SuccessData<List<Bot>>, IErrorResult>> GetBotsInMatch(long matchId)
    {
        return new SuccessData<List<Bot>>
        {
            Data = await _matchRepository.AllBots(matchId)
        };
    }

    public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> GetMatchGame(long matchId)
    {
        var res = await _matchRepository.GetGame(matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>
        {
            Data = res
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> MatchWinner(long matchId, long winner, long taskId,
        long logId, MatchResult matchResult)
    {
        return await _achievementHandlerService.MatchWinner(matchId, winner, taskId, logId, matchResult);
    }

    public async Task<HandlerResult<SuccessData<Tournament>, IErrorResult>> GetTournament(long matchId)
    {
        var res = await _matchRepository.GetTournament(matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Tournament>
        {
            Data = res
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> GameFiled(long matchId, long taskId, long gameId,
        long logId, MatchResult matchResult)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);

        var result = await _matchRepository.GetMatch(matchId);
        var taskDone = await _taskService.GetTask(taskId);
        if (result == null || taskDone.IsError) return new EntityNotFoundErrorResult();
        var task = taskDone.Match(x => x.Data, null!);
        var random = await _matchRepository.GetMatchPlayerByMatchId(matchId);
        task.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.LogId = logId;
        result.Status = GameStatus.Played;
        result.Winner = random!.BotId;
        result.MatchResult = matchResult;


        await _matchRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> GameAndBotFiled(long matchId, long loser, long taskId,
        long gameId, long logId, MatchResult matchResult)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);
        return await Loser(matchId, loser, taskId, logId, matchResult);
    }

    public async Task<HandlerResult<Success, IErrorResult>> BotFiled(long matchId, long loser, long taskId, long logId,
        MatchResult matchResult)
    {
        var winner = await _matchRepository.GetMatchPlayerByIdNoBotId(matchId, loser);
        return await _achievementHandlerService.MatchWinner(matchId, winner.BotId, taskId, logId, matchResult);
        //return await Loser(matchId, loser, taskId,logId);
    }


    private async Task<HandlerResult<Success, IErrorResult>> Loser(long matchId, long loser, long taskId, long logId,
        MatchResult matchResult)
    {
        var result = await _matchRepository.GetMatch(matchId);
        var taskDone = await _taskService.GetTask(taskId);
        if (result == null || taskDone.IsError) return new EntityNotFoundErrorResult();
        var task = taskDone.Match(x => x.Data, null!);

        var winner = await _matchRepository.GetMatchPlayerByIdNoBotId(matchId, loser);
        Console.WriteLine($"mach ide losset {matchId} {loser} {winner.Id}");
        task!.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.LogId = logId;
        result.Winner = winner.BotId;
        result.MatchResult = matchResult;

        await _achievementHandlerService.UpDateAchievement(AchievementsTypes.GamePlayed, winner.Id);
        await _achievementHandlerService.UpDateAchievement(AchievementsTypes.WinGames, winner.Id);
        var bots = await _matchRepository.GetListMatchPlayersByMatchIdAndNoBotId(matchId, winner.BotId);
        foreach (var bot in bots)
            await _achievementHandlerService.UpDateAchievement(AchievementsTypes.GamePlayed, bot.Id);

        await _matchRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<long> SaveLogGame(string log, string fileName)
    {
        return await _fileManager.SaveGameLog(log, fileName);
    }
}