using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class GameWorker: IInvocable
{
    private readonly IAchievementsRepository _achievementsRepository;
    private readonly MatchRepository _matchRepository;
    private readonly TaskRepository _taskRepository;
    public GameWorker(IAchievementsRepository achievementsRepository,MatchRepository matchRepository,TaskRepository taskRepository, long taskId)
    {
        _achievementsRepository = achievementsRepository;
        _matchRepository = matchRepository;
        _taskRepository = taskRepository;
        TaskId = taskId;
    }

    private long TaskId { get; set; }

    public async Task Invoke()
    {
        Console.WriteLine("inwoke gam workek " +TaskId);
        GameManager gameManager = new GameManager();
        _Task task = (await _taskRepository.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        var botlist = (await _matchRepository.AllBots(task.OperatingOn)).Match(x=>x.Data,x=>new List<Bot>());
        var game = (await _matchRepository.GetGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        GameResult result = await gameManager.PlayGame(game, botlist);
        SuccessfullGameResult sr = (SuccessfullGameResult) result;
        await _matchRepository.Winner(task.OperatingOn, sr.BotWinner.Id, TaskId);

    }
}