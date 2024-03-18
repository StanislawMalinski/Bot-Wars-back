using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

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
        Console.WriteLine("inwoke gam workek " +TaskId);
        GameManager gameManager = new GameManager();
        Console.WriteLine("i???sdd");
        _Task task = (await _resolver.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        Console.WriteLine("inwoke gam workek asdasdsa");
        var botlist = (await _resolver.GetBotsInMatch(task.OperatingOn)).Match(x=>x.Data,x=>new List<Bot>());
        var game = (await _resolver.GetMatchGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        Console.WriteLine("inwoke gamsdadasd");
        GameResult result = await gameManager.PlayGame(game, botlist);
        SuccessfullGameResult sr = (SuccessfullGameResult) result;
        Console.WriteLine("inwodfs");
        await _resolver.MatchWinner(task.OperatingOn, sr.BotWinner.Id, TaskId);
    }
}