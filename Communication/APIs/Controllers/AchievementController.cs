using Communication.APIs.Controllers.Helper;
using Communication.Services.Achievement;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AchievementController : Controller
{
    private readonly AchievementService _achievementService;

    public AchievementController(AchievementService achievementService)
    {
        _achievementService = achievementService;
    }


    [HttpPost("unlockAchievement")]
    public async Task<IActionResult> UnlockAchievement([FromQuery] long playerId,
        [FromQuery] long achievementTypeId, [FromQuery] long currentPlayerThreshold)
    {
        return (await _achievementService.UnlockAchievement(playerId, achievementTypeId, currentPlayerThreshold))
            .Match(Ok, this.ErrorResult);
    }

    [HttpGet("getAchievementsForPlayer")]
    public async Task<IActionResult> GetAchievementsForPlayer([FromQuery] long playerId)
    {
        return (await _achievementService.GetAchievementsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
    
    [HttpGet("getAchievementTypes")]
    public async Task<IActionResult> GetAchievementTypes()
    {
        return (await _achievementService.GetAchievementTypes()).Match(Ok, this.ErrorResult);
    }
    
}