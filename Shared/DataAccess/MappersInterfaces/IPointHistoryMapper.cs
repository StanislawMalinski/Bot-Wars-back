using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IPointHistoryMapper
{
    PointHistoryDto MapPointHistoryToPointHistoryDto(PointHistory pointHistory);
}