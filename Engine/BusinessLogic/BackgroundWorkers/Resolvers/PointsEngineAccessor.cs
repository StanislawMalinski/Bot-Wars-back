using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class PointsEngineAccessor
{
    private readonly IPointsRepository _pointsRepository;
    private readonly int EloK = 50;

    public PointsEngineAccessor(IPointsRepository pointsRepository)
    {
        _pointsRepository = pointsRepository;
    }

    private float Probability(float rating1, float rating2)
    {
        return 1.0f * 1.0f
               / (1
                  + 1.0f
                  * (float)Math.Pow(
                      10, 1.0f * (rating1 - rating2)
                          / 400));
    }


    private long EloRating(long Ra, long Rb)
    {
        var Pa = Probability(Rb, Ra);
        long points = 0;

        points = (long)Math.Round(EloK * (1 - Pa));
        points = Math.Min(points, Rb);
        return points;
    }

    public async Task<HandlerResult<Success, IErrorResult>> MatchCalculation(long winnerId, long loserId, long tourId)
    {
        if (winnerId == loserId) return new Success();
        var wp = await _pointsRepository.GetPlayerPoint(winnerId);
        var lp = await _pointsRepository.GetPlayerPoint(loserId);
        var point = EloRating(wp, lp);
        await _pointsRepository.UpdatePointsForPlayerNoSave(winnerId, point, tourId);

        await _pointsRepository.UpdatePointsForPlayerNoSave(loserId, -point, tourId);
        return new Success();
    }
}