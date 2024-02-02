using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class PointHistoryMapper : IPointHistoryMapper
{
    public PointHistoryDto? MapPointHistoryToPointHistoryDto(PointHistory? pointHistory)
    {
        if (pointHistory == null)
        {
            return null;
        }

        return new PointHistoryDto
        {
            Id = pointHistory.Id,
            Loss = pointHistory.Loss,
            Gain = pointHistory.Gain,
            PlayerId = pointHistory.PlayerId,
            LogDate = pointHistory.LogDate
        };
    }
}