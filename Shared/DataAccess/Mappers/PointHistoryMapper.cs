using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class PointHistoryMapper : IPointHistoryMapper
{
    public PointHistoryDto MapPointHistoryToPointHistoryDto(PointHistory pointHistory)
    {
        return new PointHistoryDto
        {
            Id = pointHistory.Id,
            Change = pointHistory.Change,
            Before = pointHistory.Before,
            PlayerId = pointHistory.PlayerId,
            LogDate = pointHistory.LogDate
        };
    }
}