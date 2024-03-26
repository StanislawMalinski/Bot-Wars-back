using System.Security.Claims;
using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Communication.Services.UserSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserSettingsController : Controller
{
    private readonly IUserSettingsService _userSettingsService;

    public UserSettingsController(IUserSettingsService userSettingsService)
    {
        _userSettingsService = userSettingsService;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet("getForPlayer")]
    public async Task<IActionResult> GetUserSettingsForPlayer()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await _userSettingsService.GetUserSettingsForPlayer(long.Parse(userId))).Match(Ok, this.ErrorResult);
    }
    
    [Authorize(Roles = "User,Admin")]
    [HttpPut("updateForPlayer")]
    public async Task<IActionResult> UpdateUserSettingsForPlayer( UserSettingsDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await _userSettingsService.UpdateUserSettingsForPlayer(long.Parse(userId), dto)).Match(Ok, this.ErrorResult);
    }
}