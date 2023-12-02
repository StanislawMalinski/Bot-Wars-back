using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Communication.APIs.Controllers;

[Route("api/[controller]")]
public class PointsController : Controller
{

    [HttpGet("for-player")]
    public async Task<ActionResult<ServiceResponse<long>>> GetPointsForPlayer([FromQuery]long playerId)
    {
		throw new NotImplementedException();
	}

    [HttpGet("leaderboards")]
    public async Task<ActionResult<ServiceResponse<List<long>>>> GetLeaderBoards()
    {
        throw new NotImplementedException();
    }

	[HttpGet("player-history")]
	public async Task<ActionResult<ServiceResponse<List<PointHistory>>>> GetHistoryOfPointsForPlayer([FromQuery] long playerId)
    {
        throw new NotImplementedException();
    }
}