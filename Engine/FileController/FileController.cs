using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.FileWorker;
using Engine.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;

namespace Engine.FileController;

[Route("engine/[controller]")]
[ApiController]
public class FileController : Controller
{
    private readonly TournamentResolver _resolver;

    private readonly FileManager _fileManager;
    private MatchRepository _matchRepository;
    private readonly TaskService _taskService;


    public FileController(FileManager fileManager, MatchRepository matchRepository, TaskService taskService,
        TournamentResolver resolver)
    {
        _fileManager = fileManager;
        _matchRepository = matchRepository;
        _taskService = taskService;
        _resolver = resolver;
    }

    [HttpPost("test")]
    public async Task<IActionResult> dosomthing(long tourId)
    {
        return (await _taskService.CreateTask(TaskTypes.PlayTournament, tourId, DateTime.Now)).Match(Ok, Ok);
    }


    [HttpPost("validationBot")]
    public async Task<IActionResult> dosomthing2(long botId)
    {
        return (await _taskService.CreateTask(TaskTypes.ValidateBot, botId, DateTime.Now)).Match(Ok, Ok);
    }

    // TEST - if there is file with id 5 in FileGatherer it should be obtained & saved in FileSystem
    [HttpPost("test/storage")]
    public async Task<IActionResult> TestGatherer()
    {
        var bot = new Bot
        {
            Id = 2137,
            FileId = 5,
            BotFile = "xdd.py"
        };
        var res = await _fileManager.GetBotFileFromStorage(bot);
        var res2 = res.Match(x => x.Data, x => null);
        if (res2 == null) return BadRequest(res);
        System.IO.File.WriteAllText($"FileSystem/Bots/{res2.BotId}.py", res2.FileContent);
        return Ok("Worked - file downloaded & saved");
    }

    [HttpPost("addGame")]
    public async Task<IActionResult> UploadGame(GameFileDto gameFileDto)
    {
        //return (await _fileManager.addGame(gameFileDto)).Match(Ok, Ok);
        return Ok();
    }

    [HttpGet("tourstatus")]
    public async Task<IActionResult> kopiec(long tourId)
    {
        return (await _resolver.GetTournamentMatchStatus(tourId)).Match(Ok, Ok);
    }
}