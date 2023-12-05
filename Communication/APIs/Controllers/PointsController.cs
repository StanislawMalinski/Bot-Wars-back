using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DataBaseEntities;

namespace Communication.APIs.Controllers;

[Route("api/[controller]")]
public class PointsController : Controller
{

    [HttpGet("for-player")]
    public async Task<IActionResult> GetPointsForPlayer([FromQuery]long playerId)
    {
		throw new NotImplementedException();
	}

    [HttpGet("leaderboards")]
    public async Task<IActionResult> GetLeaderBoards()
    {
        throw new NotImplementedException();
    }

	[HttpGet("player-history")]
	public async Task<IActionResult> GetHistoryOfPointsForPlayer([FromQuery] long playerId)
    {
        throw new NotImplementedException();
    }
}