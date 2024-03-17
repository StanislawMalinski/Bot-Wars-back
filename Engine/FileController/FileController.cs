using Engine.FileWorker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;
using System.IO;

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

    // TEST - if there is file with id 5 in FileGatherer it should be obtained & saved in FileSystem
    [HttpPost("test/storage")]
    public async Task<IActionResult> TestGatherer()
    {
        Bot bot = new Bot()
        {
            Id = 2137,
            FileId = 5,
            BotFile = "xdd.py"
        };
        var res = await _fileManager.GetBotFileFromStorage(bot);
        var res2 = res.Match(x => x.Data, x => null);
        if (res2 == null)
        {
            return BadRequest(res);
        }
        System.IO.File.WriteAllText($"FileSystem/Bots/{res2.BotId}.py", res2.FileContent);
        return Ok("Worked - file downloaded & saved");
    }

    [HttpPost("addGame")]
    public async Task<IActionResult> UploadGame( GameFileDto gameFileDto)
    {
        return (await _fileManager.addGame(gameFileDto)).Match(Ok, Ok);
        return Ok();
    }
}