using Shared.DataAccess.RepositoryInterfaces;

namespace Engine.Services;

public class BotService
{
    private readonly IBotRepository _botRepository;

    public BotService(IBotRepository botRepository)
    {
        _botRepository = botRepository;
    }

    
}