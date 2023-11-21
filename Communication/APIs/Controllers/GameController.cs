using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Communication.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {

        private readonly IGameServis _gameService;

        public GameController(IGameServis gameService)
        {
            _gameService = gameService;
        }
        
        /*
        [HttpGet("getGameFile")]
        public async Task<IActionResult> Gets(int id)
        {

            var result = await _gameService.GetGameAsync(id);
            if (result.Success)
            {
                
                var file = File(result.Data.Data, "application/octet-stream", result.Data.Filename);

                return file;
            }
            else
                //return StatusCode(500, $"Internal server error {result.Message}");
                return null;
            
            
        }*/


        [HttpGet("getGamessList")]
        public async Task<ActionResult<ServiceResponse<List<Game>>>> GetListAsync()
        {

            
            var result = await _gameService.GetGamesAsync();
            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, result);

        }

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


       
        [HttpDelete("deleteGame")]
        public async Task<ActionResult<ServiceResponse<Game>>> Delete(int id)
        {
            var result = await _gameService.DeleteGameAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, result);
        }
    }
}
