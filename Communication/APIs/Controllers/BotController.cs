using Communication.APIs.Controllers.Helper;
using Communication.Services.Bot;
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
    public async Task<IActionResult> GetAllBots()
    {
        return (await _botService.GetAllBots()).Match(Ok, this.ErrorResult);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddBot(BotRequest botRequest)
    {
        return (await _botService.AddBot(botRequest)).Match(Ok, this.ErrorResult);
    }
    
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
}