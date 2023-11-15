using Microsoft.AspNetCore.Mvc;
using BotWars.Services;
using BotWars.Games;
using BotWars.Services.IServices;


namespace BotWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Game>>>> GetListAsync()
        {

            
            var result = await _gameService.GetGamesAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, result.Message);
            }

        }
        
        // [HttpGet("{id:int}")]
        // public async Task<IActionResult> Gets([FromRoute]int id)
        // {
        //
        //     var result = await _gameService.GetGameAsync(id);
        //     if (result.Success)
        //     {
        //         var game = result.Data;
        //         var file = File(result.Data.Data, "application/octet-stream", result.Data.Filename);
        //
        //         return file;
        //     }
        //     else
        //         return StatusCode(500, result);
        // }

        /*
        [HttpPost("addGame")]
        public async Task<ActionResult<ServiceResponse<Game>>> Put(IFormFile file, String description)
        {
           
            var stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            Game game = new Game { Data = buffer, Description = description, Filename = file.FileName };
            stream.Close();
            var respond = await _gameService.CreateGameAsync(game);

            return Ok(respond);
        }*/


       
        // [HttpDelete("deleteGame")]
        // public async Task<ActionResult<ServiceResponse<Game>>> Delete(int id)
        // {
        //     var result = await _gameService.DeleteGameAsync(id);
        //
        //     if (result.Success)
        //         return Ok(result);
        //     else
        //         return StatusCode(500, result);
        // }
    }
}
