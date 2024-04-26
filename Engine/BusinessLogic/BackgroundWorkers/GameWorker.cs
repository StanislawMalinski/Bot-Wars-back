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
        var tour = (await _resolver.GetTournament(task.OperatingOn)).Match(x=>x.Data,null);
        var botlist = (await _resolver.GetBotsInMatch(task.OperatingOn)).Match(x=>x.Data,x=>new List<Bot>());
        var game = (await _resolver.GetMatchGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        GameResult result = await gameManager.PlayGame(game, botlist,tour.MemoryLimit,tour.TimeLimit);
        Console.WriteLine("er12345");
        if (result is SuccessfullGameResult)
        {
            Console.WriteLine("er123400");
            SuccessfullGameResult sr = (SuccessfullGameResult) result;
            await _resolver.MatchWinner(task.OperatingOn, sr.BotWinner.Id, TaskId);
            Console.WriteLine("er123400 end");
        }
        else
        {
            Console.WriteLine("er1234");
            ErrorGameResult er = (ErrorGameResult) result;
            if (er.GameError && er.BotError)
            {
                Console.WriteLine("er 0");
                await _resolver.GameAndBotFiled(task.OperatingOn, er.BotErrorId, TaskId, game.Id);
            }else if (!er.GameError && er.BotError)
            {
                Console.WriteLine("er 1");
                await _resolver.BotFiled(task.OperatingOn, er.BotErrorId, TaskId);
            }else if (er.GameError && !er.BotError)
            {
                Console.WriteLine("er 2");
                await _resolver.GameFiled(task.OperatingOn,TaskId,game.Id);
            }
            else
            {
                Console.WriteLine("er 3");
                //impossible
            }
        }
       
      
        Console.WriteLine("zakonczenie game " +TaskId);
    }
}