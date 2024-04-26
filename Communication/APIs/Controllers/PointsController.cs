using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PointsController : Controller
{
    private readonly IPointsService _pointsService;

    public PointsController(IPointsService pointsService)
    {
        _pointsService = pointsService;
    }
    
    [HttpPost("setPointsForPlayer")]
    public async Task<IActionResult> SetPointsForPlayer([FromQuery] long playerId, [FromQuery] long points)
    {
        return (await _pointsService.SetPointsForPlayer(playerId, points)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("getPointsForPlayer")]
    public async Task<IActionResult> GetPointsForPlayer([FromQuery] long playerId)
    {
		return (await _pointsService.GetPointsForPlayer(playerId)).Match(Ok, this.ErrorResult);
	}

    [HttpGet("getLeaderboards")]
    public async Task<IActionResult> GetLeaderboards([FromQuery] int page = 0, [FromQuery] int pagesize = 10)
    {
        return (await _pointsService.GetLeaderboards(page, pagesize)).Match(Ok, this.ErrorResult);
    }

	[HttpGet("getPointsHistoryForPlayer")]
	public async Task<IActionResult> GetHistoryOfPointsForPlayer([FromQuery] long playerId)
    {
        return (await _pointsService.GetHistoryForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
}