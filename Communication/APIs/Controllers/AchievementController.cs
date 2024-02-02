using Communication.APIs.Controllers.Helper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AchievementController : Controller
{
    private readonly IAchievementsRepository _achievementsRepository;

    public AchievementController(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    [HttpPost("unlockAchievement")]
    public async Task<IActionResult> UnlockAchievement([FromQuery] long playerId,
        [FromQuery] long achievementTypeId, [FromQuery] long currentPlayerThreshold)
    {
        return (await _achievementsRepository.UnlockAchievement(playerId, achievementTypeId, currentPlayerThreshold))
            .Match(Ok, this.ErrorResult);
    }

    [HttpGet("getAchievementsForPlayer")]
    public async Task<IActionResult> GetAchievementsForPlayer([FromQuery] long playerId)
    {
        return (await _achievementsRepository.GetAchievementsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
    
    [HttpGet("getAchievementTypes")]
    public async Task<IActionResult> GetAchievementTypes()
    {
        return (await _achievementsRepository.GetAchievementTypes()).Match(Ok, this.ErrorResult);
    }
    
}