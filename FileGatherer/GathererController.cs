using Microsoft.AspNetCore.Mvc;

namespace FileGatherer
{
    [Route("api/[controller]")]
    [ApiController]

    public class GathererController : Controller
    {
        private readonly GathererService _gathererService;

        public GathererController(GathererService gathererService)
        {
            _gathererService = gathererService;
        }

        [HttpPut("game")]
        public async Task<IActionResult> UploadGameFile()
        {
            var file = Request.Form.Files[0];

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var result = await _gathererService.SaveGameFile(file);
            if (result.Success)
            {
                return Ok(result.Data);
            } 
            else 
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
        }

        [HttpPut("bot")]
        public async Task<IActionResult> UploadBotFile()
        {
            var file = Request.Form.Files[0];

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var result = await _gathererService.SaveBotFile(file);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
        }

        [HttpGet("game")]
        public async Task<IActionResult> GetGameFile([FromBody] string fileName)
        {
            var result = await _gathererService.GetGameFile(fileName);
            if (!result.Success)
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
            return File(result.Data.Content, "application/octet-stream", result.Data.Name);
        }

        [HttpGet("bot")]
        public async Task<IActionResult> GetFile([FromBody] string fileName)
        {
            var result = await _gathererService.GetBotFile(fileName);
            if (!result.Success)
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
            return File(result.Data.Content, "application/octet-stream", result.Data.Name);
        }
    }
}
