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
    private readonly TaskService _taskService;
    private readonly MatchRepository _matchRepository;
    private readonly AchievementHandlerService _achievementHandlerService;
    private readonly IGameRepository _gameRepository;

    public MatchResolver(TaskService taskService, MatchRepository matchRepository, AchievementHandlerService achievementHandlerService, IGameRepository gameRepository)
    {
        _taskService = taskService;
        _matchRepository = matchRepository;
        _achievementHandlerService = achievementHandlerService;
        _gameRepository = gameRepository;
    }


    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await _taskService.GetTask(taskId);
    }
    public async Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetBotsInMatch(long matchId)
    {
        return new SuccessData<List<Bot>>()
        {
            Data = await _matchRepository.AllBots(matchId)
        };
    }

    public async  Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetMatchGame(long matchId)
    {
        var res= await _matchRepository.GetGame(matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>()
        {
            Data = res
        };
    }

    public async  Task<HandlerResult<Success,IErrorResult>> MatchWinner(long matchId, long winner, long taskId)
    {
        return await _achievementHandlerService.MatchWinner(matchId, winner, taskId);
    }

    public async Task<HandlerResult<SuccessData<Tournament>, IErrorResult>> GetTournament(long matchId)
    {
        var res =  await _matchRepository.GetTournament(matchId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Tournament>()
        {
            Data = res
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> GameFiled(long matchId,long taskId,long gameId)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);

        var result = await _matchRepository.GetMatch(matchId);
        var taskDone = await _taskService.GetTask(taskId);
        if (result == null || taskDone.IsError) return new EntityNotFoundErrorResult();
        var task = taskDone.Match(x => x.Data, null!);
        var random = await _matchRepository.GetMatchPlayerByMatchId(matchId);
        task.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = random!.Id;


        await _matchRepository.SaveChangesAsync();
        return new Success();
        
    } 
    
    public async Task<HandlerResult<Success, IErrorResult>> GameAndBotFiled(long matchId,long loser,long taskId,long gameId)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);
        return await Loser(matchId, loser, taskId);
    } 
    
    public async Task<HandlerResult<Success, IErrorResult>> BotFiled(long matchId,long loser,long taskId)
    {
        return await Loser(matchId, loser, taskId);
    } 
    
    
    private  async Task<HandlerResult<Success, IErrorResult>> Loser(long matchId, long loser, long taskId)
    {
        var result = await _matchRepository.GetMatch(matchId);
        var taskDone = await _taskService.GetTask(taskId);
        if (result == null || taskDone.IsError) return new EntityNotFoundErrorResult();
        var task = taskDone.Match(x => x.Data, null!);
        var winner = await _matchRepository.GetMatchPlayerByIdNoBotId(matchId, loser);
        
        task!.Status = TaskStatus.Done;
        result.Played = DateTime.Now;
        result.Status = GameStatus.Played;
        result.Winner = winner.Id;
        await _achievementHandlerService.UpDateAchievement(AchievementsTypes.GamePlayed, winner.Id);
        await _achievementHandlerService.UpDateAchievement(AchievementsTypes.WinGames, winner.Id);
        var bots = await _matchRepository.GetListMatchPlayersByMatchIdAndNoBotId(matchId, winner.BotId);
        foreach (var bot in bots)
        {
            await _achievementHandlerService.UpDateAchievement(AchievementsTypes.GamePlayed, bot.Id);
        }

        await _matchRepository.SaveChangesAsync();
        return new Success();
    }
}