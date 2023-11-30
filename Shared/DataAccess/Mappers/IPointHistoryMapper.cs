using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IPointHistoryMapper
{
    PointHistoryDto? MapPointHistoryToPointHistoryDto(PointHistory? pointHistory);
    PointHistory? MapPointHistoryDtoToPointHistory(PointHistoryDto? pointHistoryDto);
}