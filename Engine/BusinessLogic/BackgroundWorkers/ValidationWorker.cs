using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.BusinessLogic.Gameplay;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class ValidationWorker : IInvocable
{
    private readonly ValidationResolver _resolver;
    private readonly long _taskId;

    public ValidationWorker(ValidationResolver validationResolver, long taskId)
    {
        _resolver = validationResolver;
        _taskId = taskId;
    }

    public async Task Invoke()
    {
        Console.WriteLine("validanion begin");
        var gameManager = new GameManager();
        var task = (await _resolver.GetTask(_taskId)).Match(x => x.Data, x => null);
        if (task == null) return;
        Console.Write("validanion ffds begin");
        var bot = (await _resolver.GetBot(task.OperatingOn)).Match(x => x.Data, x => null);
        var game = (await _resolver.GetGame(task.OperatingOn)).Match(x => x.Data, x => null);
        if (bot == null || game == null) return;
        var result = await gameManager.PlayGame(game, new List<Bot>
        {
            bot,
            bot
        });
        var res = gameManager.GetBotsPerformers();
        await _resolver.ValidateBot(_taskId, result is SuccessfullGameResult,
            Math.Max(res[0].MemoryUse, res[1].MemoryUse), Math.Max(res[0].TimeUse, res[1].TimeUse));

        Console.WriteLine("validanion endende");
    }
}