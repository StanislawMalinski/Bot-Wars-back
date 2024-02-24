using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class GameWorker: IInvocable, IInvocableWithPayload<GameData>
{
    private IAchievementsRepository _achievementsRepository;
    private MatchRepository _matchRepository;
    public GameWorker(IAchievementsRepository achievementsRepository,MatchRepository matchRepository)
    {
        _achievementsRepository = achievementsRepository;
        _matchRepository = matchRepository;
    }

    public GameData Payload { get; set; }

    public async Task Invoke()
    {
        GameManager gameManager = new GameManager();
        GameResult result = await gameManager.PlayGame(Payload.Game,Payload.BotsId);
        /*
        _matchRepository.IsMatchPlayed()*/
        
    }
}