using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class MatchResolver : Resolver
{
    private readonly TaskRepository _taskRepository;
    private readonly MatchRepository _matchRepository;
    private readonly AchievementHandlerService _achievementHandlerService;
    private readonly IGameRepository _gameRepository;

    public MatchResolver(TaskRepository taskRepository, MatchRepository matchRepository, AchievementHandlerService achievementHandlerService, IGameRepository gameRepository)
    {
        _taskRepository = taskRepository;
        _matchRepository = matchRepository;
        _achievementHandlerService = achievementHandlerService;
        _gameRepository = gameRepository;
    }

    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await _taskRepository.GetTask(taskId);
    }
    public async Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetBotsInMatch(long matchId)
    {
        return await _matchRepository.AllBots(matchId);
    }

    public async  Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetMatchGame(long matchId)
    {
        return await _matchRepository.GetGame(matchId);
    }

    public async  Task<HandlerResult<Success,IErrorResult>> MatchWinner(long matchId, long winner, long taskId)
    {
        return await _achievementHandlerService.MatchWinner(matchId, winner, taskId);
    }

    public async Task<HandlerResult<SuccessData<Tournament>, IErrorResult>> GetTournament(long matchId)
    {
        return await _matchRepository.GetTournament(matchId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> GameFiled(long matchId,long taskId,long gameId)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);
        return await _matchRepository.GameFiled(matchId, taskId);
    } 
    
    public async Task<HandlerResult<Success, IErrorResult>> GameAndBotFiled(long matchId,long loser,long taskId,long gameId)
    {
        await _gameRepository.GameNotAvailableForPlay(gameId);
        return await _matchRepository.Loser(matchId, loser, taskId);
    } 
    
    public async Task<HandlerResult<Success, IErrorResult>> BotFiled(long matchId,long loser,long taskId)
    {
        return await _matchRepository.Loser(matchId, loser, taskId);
    } 
}