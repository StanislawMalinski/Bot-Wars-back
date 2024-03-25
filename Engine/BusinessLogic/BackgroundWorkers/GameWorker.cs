using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class GameWorker: IInvocable
{
    private readonly MatchResolver _resolver;
    public GameWorker(MatchResolver matchResolver, long taskId)
    {
        _resolver = matchResolver;
        TaskId = taskId;
    }

    private long TaskId { get; set; }

    public async Task Invoke()
    {
        Console.WriteLine("game worker " +TaskId);
        GameManager gameManager = new GameManager();
        _Task task = (await _resolver.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        if (task.Status == TaskStatus.Done) return;
        var botlist = (await _resolver.GetBotsInMatch(task.OperatingOn)).Match(x=>x.Data,x=>new List<Bot>());
        var game = (await _resolver.GetMatchGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        GameResult result = await gameManager.PlayGame(game, botlist);
        SuccessfullGameResult sr = (SuccessfullGameResult) result;
        await _resolver.MatchWinner(task.OperatingOn, sr.BotWinner.Id, TaskId);
        Console.WriteLine("zakonczenie game " +TaskId);
    }
}