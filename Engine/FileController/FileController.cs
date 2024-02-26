using Engine.FileWorker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;

namespace Engine.FileController;

[Route("engine/[controller]")]
[ApiController]
public class FileController : Controller
{

    private FileManager _fileManager;
    private MatchRepository _matchRepository;

    public FileController(FileManager fileManager,MatchRepository matchRepository)
    {
        _fileManager = fileManager;
        _matchRepository = matchRepository;
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> UploadBot( BotFileDto botDto)
    {
        return (await _fileManager.addbot(botDto)).Match(Ok, Ok);
        return Ok();
    }
    
    [HttpPost("commpile")]
    public async Task<IActionResult> UploadXDBot()
    {
        await _fileManager.bottest();
        return Ok();
    }
    
     
    [HttpPost("test")]
    public async Task<IActionResult> dosomthing()
    {


        return (await _matchRepository.RestoreLather(1)).Match(Ok,Ok);
        return Ok();
        GameInfo gameInfo = new GameInfo(true,2,null,new List<Bot>()
        {
          new Bot()
          {
              Id = 1
          } ,
          new Bot()
          {
              Id = 5
          } 
        });
        await _matchRepository.CreateMatch(1,gameInfo);
        return Ok();
    }
    
   
    [HttpPost("addGame")]
    public async Task<IActionResult> UploadGame( GameFileDto gameFileDto)
    {
        return (await _fileManager.addGame(gameFileDto)).Match(Ok, Ok);
        return Ok();
    }
    
    
}