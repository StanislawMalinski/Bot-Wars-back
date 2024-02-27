using Coravel.Invocable;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class ValidationWorker: IInvocable
{
    public ValidationWorker(BotRepository botRepository, TaskRepository taskRepository, long taskId)
    {
        _botRepository = botRepository;
        _taskRepository = taskRepository;
        _taskId = taskId;
    }

    private readonly BotRepository _botRepository;
    private readonly TaskRepository _taskRepository;
    private readonly long _taskId;
    
    public async Task Invoke()
    {
        GameManager gameManager = new GameManager();
        _Task? task = (await _taskRepository.GetTask(_taskId)).Match(x=>x.Data,x=>null);
        if(task == null) return;
        var bot = (await _botRepository.GetBot(task.OperatingOn)).Match(x=>x.Data,x=>null);
        var game = (await _botRepository.GetGame(task.OperatingOn)).Match(x=>x.Data,x=>null);
        GameResult result = await gameManager.PlayGame(game, new List<Bot>()
        {
            bot,
            bot
        });
        await _botRepository.ValidationResult(_taskId, result is SuccessfullGameResult);
    }
}