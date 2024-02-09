using Communication.APIs.Controllers.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.Repositories;

namespace Communication.APIs.Controllers;

[Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
[ApiController]
public class BotController : Controller
{
    private readonly BotRepository _repository;

    public BotController(BotRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("add")]
    public async Task<IActionResult> UploadBot( BotDto botDto)
    {
        return  (await _repository.addBot(botDto)).Match(Ok, this.ErrorResult);
        return Ok();
    }
    
    
}

