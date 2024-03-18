using Communication.APIs.Controllers.Helper;
using Communication.Services.UserSettings;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserSettingsController : Controller
{
    private readonly UserSettingsService _userSettingsService;

    public UserSettingsController(UserSettingsService userSettingsService)
    {
        _userSettingsService = userSettingsService;
    }

    [HttpGet("getForPlayer")]
    public async Task<IActionResult> GetUserSettingsForPlayer([FromQuery] long playerId)
    {
        return (await _userSettingsService.GetUserSettingsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }

    [HttpPost("createForPlayer")]
    public async Task<IActionResult> CreateUserSettingsForPlayer([FromQuery] long playerId)
    {
        return (await _userSettingsService.CreateUserSettingsForPlayer(playerId)).Match(Ok, this.ErrorResult);
    }

    [HttpPut("updateForPlayer")]
    public async Task<IActionResult> UpdateUserSettingsForPlayer([FromQuery] long playerId,
        [FromBody] UserSettingsDto dto)
    {
        return (await _userSettingsService.UpdateUserSettingsForPlayer(playerId, dto)).Match(Ok, this.ErrorResult);
    }
}