using Engine.FileWorker;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;
using System.IO;
using Shared.DataAccess.Enumerations;

namespace Engine.FileController;

[Route("engine/[controller]")]
[ApiController]
public class FileController : Controller
{

    private FileManager _fileManager;
    private MatchRepository _matchRepository;
    private TaskRepository _taskRepository;

    public FileController(FileManager fileManager,MatchRepository matchRepository, TaskRepository taskRepository)
    {
        _fileManager = fileManager;
        _matchRepository = matchRepository;
        _taskRepository = taskRepository;
    }
    
     
    [HttpPost("test")]
    public async Task<IActionResult> dosomthing()
    {
        
        return (await  _taskRepository.CreateTask(TaskTypes.PlayTournament, 1L, DateTime.Now)).Match(Ok,Ok);
        
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