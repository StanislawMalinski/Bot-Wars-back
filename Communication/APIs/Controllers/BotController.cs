using System.Security.Claims;
using Communication.APIs.Controllers.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.Pagination;
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

    [Authorize(Roles = "User,Admin")]
    [HttpPost("add")]
    public async Task<IActionResult> AddBot(BotRequest botRequest)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await _botService.AddBot(botRequest, long.Parse(userId!))).Match(Ok, this.ErrorResult);
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
    public async Task<IActionResult> GetBotsForPlayer([FromQuery] string playerName,
        [FromQuery] PageParameters pageParameters)
    {
        return (await _botService.GetBotsForPlayer(playerName, pageParameters)).Match(Ok, this.ErrorResult);
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet("getBotFileForPlayer")]
    public async Task<IActionResult> GetBotFileForPlayer([FromQuery] long botId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var res = await _botService.GetBotFileForPlayer(long.Parse(userId!), botId);
        return res.Match(
            x => File(x.Data.OpenReadStream(), x.Data.ContentType, x.Data.FileName),
            this.ErrorResult
        );
    }

    [HttpGet("getBotsForTournament")]
    public async Task<IActionResult> GetBotsForTournament([FromQuery] long tournamentId,
        [FromQuery] PageParameters pageParameters)
    {
        return (await _botService.GetBotsForTournament(tournamentId, pageParameters)).Match(Ok, this.ErrorResult);
    }
}