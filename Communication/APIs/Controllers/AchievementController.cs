﻿using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Communication.Services.Achievement;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AchievementController : Controller
{
    private readonly IAchievementService _achievementService;

    public AchievementController(IAchievementService achievementService)
    {
        _achievementService = achievementService;
    }

    [HttpGet("getAchievementsForPlayer")]
    public async Task<IActionResult> GetAchievementsForPlayer([FromQuery] long playerId)
    {
        return (await _achievementService.GetAchievementsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
    
    [HttpGet("getAllAchievementsForPlayer")]
    public async Task<IActionResult> GetAllAchievementsForPlayer([FromQuery] long playerId)
    {
        return (await _achievementService.GetAllAchievementsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
    
    [HttpGet("getAchievementTypes")]
    public async Task<IActionResult> GetAchievementTypes()
    {
        return (await _achievementService.GetAchievementTypes()).Match(Ok, this.ErrorResult);
    }
    
    [HttpGet("getAchivmentIcon")]
    public async Task<IActionResult> getAchivmentIcon()
    {
        return null;
    }
    
}