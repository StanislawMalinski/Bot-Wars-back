using Shared.DataAccess.Context;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class AchievementHandlerService
{
    private readonly DataContext _dataContext;

    public AchievementHandlerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
}