using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Shared.DataAccess.Mappers;

public interface IPointHistoryMapper
{
    PointHistoryDto MapPointHistoryToPointHistoryDto(PointHistory pointHistory);
}