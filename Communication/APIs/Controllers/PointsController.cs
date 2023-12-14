using Communication.APIs.Controllers.Helper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/[controller]")]
public class PointsController : Controller
{
    private readonly IPointsService _pointsService;

    public PointsController(IPointsService pointsService)
    {
        _pointsService = pointsService;
    }

    [HttpGet("for-player")]
    public async Task<IActionResult> GetPointsForPlayer([FromQuery]long playerId)
    {
		return (await _pointsService.GetPointsForPlayer(playerId)).Match(Ok, this.ErrorResult);
	}

    [HttpGet("leaderboards")]
    public async Task<IActionResult> GetLeaderBoards()
    {
        return (await _pointsService.GetLeaderboards()).Match(Ok, this.ErrorResult);
    }

	[HttpGet("player-history")]
	public async Task<IActionResult> GetHistoryOfPointsForPlayer([FromQuery] long playerId)
    {
        return (await _pointsService.GetHistoryForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
}