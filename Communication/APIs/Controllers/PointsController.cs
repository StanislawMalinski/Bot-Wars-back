using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Communication.APIs.Controllers;

[Route("api/[controller]")]
public class PointsController : Controller
{

    public async Task<ActionResult<ServiceResponse<long>>> GetPointsForPlayer([FromQuery]long playerId)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ServiceResponse<List<long>>>> GetLeaderBoards()
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<ServiceResponse<List<PointHistory>>>> GetHistoryOfPointsForPlayer(
        [FromQuery] long playerId)
    {
        throw new NotImplementedException();
    }
}