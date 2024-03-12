using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class TournamentResolver : Resolver
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly TaskRepository _taskRepository;
    private readonly MatchRepository _matchRepository;
    private readonly AchievementHandlerService _achievementHandlerService;
    
    public TournamentResolver(TournamentRepository tournamentRepository, TaskRepository taskRepository, AchievementHandlerService achievementHandlerService, MatchRepository matchRepository)
    {
        _tournamentRepository = tournamentRepository;
        _taskRepository = taskRepository;
        _achievementHandlerService = achievementHandlerService;
        _matchRepository = matchRepository;
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
        return await _taskRepository.GetTask(taskId);
    }

    public async  Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetGame(long tourId)
    {
        return await _tournamentRepository.TournamentGame(tourId);
    }

    public async Task<HandlerResult<Success,IErrorResult>> AreAnyMatchesPlayed(long tourId)
    {
        return await _matchRepository.AreAny(tourId);
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
        return await _matchRepository.CreateAllLadder(matches, tourId);
    }
    
    public async Task<HandlerResult<SuccessData<List<long>>,IErrorResult>> GetMatchesReadyToPlay(long tourId)
    {
        return await _matchRepository.GetAllReadyToPlay(tourId);
    }
   
    public async Task<HandlerResult<Success,IErrorResult>> PlayMatch(long matchId)
    {
        return await  _matchRepository.PlayMatch(matchId);
    }
    
    public async Task<HandlerResult<SuccessData<List<long>>,IErrorResult>> GetPlayedMatches(long tourId)
    {
        return await _matchRepository.GetAllPlayed(tourId);
    }
    
    public async Task<HandlerResult<Success,IErrorResult>> ResolveMatch(long matchId)
    {
        return await _matchRepository.ResolveMatch(matchId);
    }
    
    public async Task<HandlerResult<SuccessData<long>,IErrorResult>> IsMatchResolved(long tourId,string key)
    {
        return await _matchRepository.IsResolve(tourId,key);
    }
    
}