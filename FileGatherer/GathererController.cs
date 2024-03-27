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

        [HttpPut]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files[0];

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var result = await _gathererService.SaveFile(file);
            if (result.Success)
            {
                return Ok(result.Data);
            } 
            else 
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile([FromRoute] long id)
        {
            var result = await _gathererService.GetFile(id);
            if (!result.Success)
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
            return File(result.Data.Content, "application/octet-stream", result.Data.Name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile([FromRoute] long id)
        {
            var result = await _gathererService.DeleteFile(id);
            if (!result.Success)
            {
                return StatusCode(500, $"Internal server error: {result.Message}");
            }
            return Ok(result.Message);
        }
    }
}
