using Engine.FileWorker;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;

namespace Engine.FileController;

[Route("engine/[controller]")]
[ApiController]
public class FileController : Controller
{

    private FileManager _fileManager;

    public FileController(FileManager fileManager)
    {
        _fileManager = fileManager;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> UploadBot( BotFileDto botDto)
    {
        return (await _fileManager.addbot(botDto)).Match(Ok, Ok);
        return Ok();
    }
    
   
    [HttpPost("addGame")]
    public async Task<IActionResult> UploadGame( GameFileDto gameFileDto)
    {
        return (await _fileManager.addGame(gameFileDto)).Match(Ok, Ok);
        return Ok();
    }
    
    
}