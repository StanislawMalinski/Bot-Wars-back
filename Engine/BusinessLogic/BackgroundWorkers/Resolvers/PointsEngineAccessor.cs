using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class PointsEngineAccessor
{
    private readonly TournamentRepository _tournamentRepository;
    private readonly AchievementHandlerService _achievementHandler;

    public PointsEngineAccessor(TournamentRepository tournamentRepository, AchievementHandlerService achievementHandler)
    {
        _tournamentRepository = tournamentRepository;
        _achievementHandler = achievementHandler;
    }
}