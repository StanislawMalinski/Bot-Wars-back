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
    private IAchievementsRepository _achievementsRepository;
    private MatchRepository _matchRepository;
    private TaskRepository _taskRepository;
    public GameWorker(IAchievementsRepository achievementsRepository,MatchRepository matchRepository,TaskRepository taskRepository, long taskid)
    {
        _achievementsRepository = achievementsRepository;
        _matchRepository = matchRepository;
        _taskRepository = taskRepository;
        Payload = taskid;
    }

    public long Payload { get; set; }

    public async Task Invoke()
    {
        Console.WriteLine("inwoke gam workek " +Payload);
        GameManager gameManager = new GameManager();
        _Task task = (await _taskRepository.StartTask(Payload)).Match(x=>x.Data,x=>null);
        var botlist = (await _matchRepository.AllBots(task.OperatingOn)).Match(x=>x.Data,x=>new List<Bot>());
        var game = (await _matchRepository.GetGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        GameResult result = await gameManager.PlayGame(game, botlist);
        SuccessfullGameResult sr = (SuccessfullGameResult) result;
        await _matchRepository.Winner(task.OperatingOn, sr.BotWinner.Id);
        //GameResult result = await gameManager.PlayGame(Payload.Game,Payload.BotsId);
        /*
        _matchRepository.IsMatchPlayed()*/
        await _taskRepository.TaskComplete(Payload);

    }
}