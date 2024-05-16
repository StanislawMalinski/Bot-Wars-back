using Engine.FileWorker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;
using System.IO;
using Engine.Services;
using Shared.DataAccess.Enumerations;

namespace Engine.FileController;

[Route("engine/[controller]")]
[ApiController]
public class FileController : Controller
{

    private FileManager _fileManager;
    private MatchRepository _matchRepository;
    private TaskService _taskService;


    public FileController(FileManager fileManager, MatchRepository matchRepository, TaskService taskService)
    {
        _fileManager = fileManager;
        _matchRepository = matchRepository;
        _taskService = taskService;
    }

    [HttpPost("test")]
    public async Task<IActionResult> dosomthing(long tourId)
    {
        
        return (await  _taskService.CreateTask(TaskTypes.PlayTournament, tourId, DateTime.Now)).Match(Ok,Ok);
        
    }

    
    [HttpPost("validationBot")]
    public async Task<IActionResult> dosomthing2(long botId)
    {
        
        return (await  _taskService.CreateTask(TaskTypes.ValidateBot, botId, DateTime.Now)).Match(Ok,Ok);
        
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
        //return (await _fileManager.addGame(gameFileDto)).Match(Ok, Ok);
        return Ok();
    }
}