using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class GameWorker : IInvocable
{
    private readonly MatchResolver _resolver;

    public GameWorker(MatchResolver matchResolver, long taskId)
    {
        _resolver = matchResolver;
        TaskId = taskId;
    }

    private long TaskId { get; }

    public async Task Invoke()
    {
        Console.WriteLine("game_worker_0 " + TaskId);
        var gameManager = new GameManager();
        var task = (await _resolver.GetTask(TaskId)).Match(x => x.Data, x => null);
        Console.WriteLine("game_worker_1 " + TaskId);
        if (task.Status == TaskStatus.Done) return;
        Console.WriteLine("game_worker_2 " + TaskId);
        var tour = (await _resolver.GetTournament(task.OperatingOn)).Match(x => x.Data, null);
        Console.WriteLine("game_worke_3 " + TaskId);
        var botlist = (await _resolver.GetBotsInMatch(task.OperatingOn)).Match(x => x.Data, x => new List<Bot>());
        Console.WriteLine("game_worker_4 " + TaskId);
        var game = (await _resolver.GetMatchGame(task.OperatingOn)).Match(x => x.Data, x => null);
        Console.WriteLine("game_worker_5 " + TaskId);
        var result = await gameManager.PlayGame(game, botlist, tour.MemoryLimit, tour.TimeLimit);
        Console.WriteLine("game_worker_6 " + TaskId);
        Console.WriteLine("er12345 " + TaskId);
        var lr = await _resolver.SaveLogGame(result.gameLog, $"{TaskId} hejo elo{TaskId}");
        Console.WriteLine("game_worker_7 " + TaskId);
        Console.WriteLine($"{TaskId} Zapis_logyu_gry_pod_id {lr}");
        var saa = result.gameLog;
        if (result is SuccessfullGameResult)
        {
            Console.WriteLine("game_worker_8 gra zakończona skucesem " + TaskId);
            var sr = (SuccessfullGameResult)result;
            await _resolver.MatchWinner(task.OperatingOn, sr.BotWinner.Id, TaskId, lr, MatchResult.Won);
            Console.WriteLine("game_worker_8 zapisanie zwyciescy " + TaskId);
        }
        else
        {
            Console.WriteLine("game_worker_9 zakonczenie z błędem " + TaskId);
            var er = (ErrorGameResult)result;
            if (er.GameError && er.BotError)
            {
                Console.WriteLine("typ błędu 1 0");
                await _resolver.GameAndBotFiled(task.OperatingOn, er.BotErrorId, TaskId, game.Id, lr,
                    MatchResult.GameAndBotFiled);
            }
            else if (!er.GameError && er.BotError)
            {
                Console.WriteLine("typ błędu 1");
                MatchResult res;
                switch (er.ErrorGameStatus)
                {
                    case ErrorGameStatus.InternalError:
                        res = MatchResult.BotFiled;
                        break;
                    case ErrorGameStatus.MemoryLimit:
                        res = MatchResult.BotMemoryFiled;
                        break;
                    case ErrorGameStatus.TimeLimit:
                        res = MatchResult.BotTimeFiled;
                        break;
                    default:
                        res = MatchResult.BotFiled;
                        break;
                }

                await _resolver.BotFiled(task.OperatingOn, er.BotErrorId, TaskId, lr, res);
            }
            else if (er.GameError && !er.BotError)
            {
                Console.WriteLine("typ błędu 2");
                await _resolver.GameFiled(task.OperatingOn, TaskId, game.Id, lr, MatchResult.GameFiled);
            }
            else
            {
                Console.WriteLine("typ błędu 3");
                //impossible
            }
        }


        Console.WriteLine("game_worker_9 koniec po błędzie  " + TaskId);
    }
}