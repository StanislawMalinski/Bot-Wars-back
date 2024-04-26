using System.Security.Claims;
using Communication.APIs.Controllers.Helper;
using Communication.Services.Bot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BotController : Controller
{
    private readonly IBotService _botService;

    public BotController(IBotService botService)
    {
        _botService = botService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllBots(long playerId)
    {
        return (await _botService.GetAllBots()).Match(Ok, this.ErrorResult);
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost("add")]
    public async Task<IActionResult> AddBot(BotRequest botRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await _botService.AddBot(botRequest,long.Parse(userId!))).Match(Ok, this.ErrorResult);
    }

    [Authorize(Roles = "User,Admin")]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteBot([FromQuery] long botId)
    {
        return (await _botService.DeleteBot(botId)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("getOne")]
    public async Task<IActionResult> GetBot([FromQuery] long botId)
    {
        return (await _botService.GetBotResponse(botId)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("getForPlayer")]
    public async Task<IActionResult> GetBotsForPlayer([FromQuery] long playerId)
    {
        return (await _botService.GetBotsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }
    [Authorize(Roles = "User,Admin")]
    [HttpGet("getBotFileForPlayer")]
    public async Task<IActionResult> GetBotFileForPlayer( [FromQuery] long botId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await _botService.GetBotFileForPlayer(long.Parse(userId!), botId)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("getBotsForTournament")]
    public async Task<IActionResult> GetBotsForTournament([FromQuery] long tournamentId)
    {
        return (await _botService.GetBotsForTournament(tournamentId)).Match(Ok, this.ErrorResult);
    }
}