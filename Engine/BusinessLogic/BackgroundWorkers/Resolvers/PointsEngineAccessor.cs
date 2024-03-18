using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class PointsEngineAccessor
{

    private readonly IPointsRepository _pointsRepository;
    private int EloK = 50;

    public PointsEngineAccessor( IPointsRepository pointsRepository)
    {
        _pointsRepository = pointsRepository;
    }
    private float Probability(float rating1, float rating2)
    {
        return 1.0f * 1.0f
               / (1
                  + 1.0f
                  * (float)(Math.Pow(
                      10, 1.0f * (rating1 - rating2)
                          / 400)));
    }


    private long EloRating(long Ra, long Rb)
    {
        
        float Pa = Probability(Rb, Ra);
        long points = 0;
        
        points = (long)Math.Round(EloK * (1 - Pa));
        points = Math.Min(points, Rb);
        return points;
    }

    public async Task<HandlerResult<Success, IErrorResult>> MatchCalculation(long winnerId,long loserId)
    {
        if (winnerId == loserId) return new Success();
        long wp = (await _pointsRepository.GetPlayerPoint(winnerId)).Match(x => x.Data, x => 0);
        long lp = (await _pointsRepository.GetPlayerPoint(loserId)).Match(x => x.Data, x => 0);
        long point = EloRating(wp, lp);
        var res = await _pointsRepository.UpdatePointsForPlayerNoSave(winnerId, point);
        if (res.IsError) return res;
        res = await _pointsRepository.UpdatePointsForPlayerNoSave(loserId, -point);
        return res;
    }
}