using Shared.DataAccess.Context;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class AchievementHandlerService
{
  
    private readonly MatchRepository _matchRepository;
    private readonly PointsEngineAccessor _pointsEngineAccessor;
    private readonly TournamentRepository _tournamentRepository;

    public AchievementHandlerService(MatchRepository matchRepository, PointsEngineAccessor pointsEngineAccessor, TournamentRepository tournamentRepository)
    {
        _matchRepository = matchRepository;
        _pointsEngineAccessor = pointsEngineAccessor;
        _tournamentRepository = tournamentRepository;
    }
    
    public async  Task<HandlerResult<Success,IErrorResult>> MatchWinner(long matchId, long winner, long taskId)
    {
        var losers = (await _matchRepository.GetAllLosers(matchId, winner)).Match(x=>x.Data,x=>new List<long>());
        var playerWinner = (await _matchRepository.GetPlayerFromBot(winner)).Match(x => x.Data, x => 0);
        var tour = (await _matchRepository.GetTournament(matchId)).Match(x=>x.Data,null!);
        foreach (var loser in losers!)
        {
            await _pointsEngineAccessor.MatchCalculation(playerWinner, loser,tour!.Id);
        }
        return await _matchRepository.Winner( matchId, winner, taskId);
        
    }
    
    public async Task<HandlerResult<Success,IErrorResult>> EndTournament(long tourId,long botId, long taskId)
    {
        return await _tournamentRepository.TournamentEnded(tourId,botId,taskId);
    }
}