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
            Player = pointHistory.Player,
            LogDate = pointHistory.LogDate
        };
    }
    
    public PointHistory? MapPointHistoryDtoToPointHistory(PointHistoryDto? pointHistoryDto)
    {
        if (pointHistoryDto == null)
        {
            return null;
        }

        return new PointHistory
        {
            Id = pointHistoryDto.Id,
            Loss = pointHistoryDto.Loss,
            Gain = pointHistoryDto.Gain,
            PlayerId = pointHistoryDto.PlayerId,
            Player = pointHistoryDto.Player,
            LogDate = DateTime.Now
        };
    }
}