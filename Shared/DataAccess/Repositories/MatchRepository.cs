using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class MatchRepository
{
    private readonly DataContext _dataContext;

    public MatchRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> IsMatchPlayed(long tourId, string identifier)
    {
        var result = await _dataContext.Matches.FirstOrDefaultAsync(x =>
            x.TournamentsId == tourId && x.Status == GameStatus.Played && x.Data.Equals(identifier));
        if(result == null) return new EntityNotFoundErrorResult();
        var bot = await _dataContext.Bots.FirstOrDefaultAsync(x => x.Id == result.Winner);
        if(bot == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Bot>()
        {
            Data = bot
        };
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> CreateMatch(long tourId, List<Bot> bots, long gameId)
    {
        Matches match = new Matches();
        match.TournamentsId = tourId;
        match.GameId = gameId;
        match.Status = GameStatus.ReadyToPlay;
        var res = await _dataContext.Matches.AddAsync(match);

        Console.WriteLine("przed pentlą");
        foreach (var bot in bots)
        {
            MatchPlayers players = new MatchPlayers();
            players.Matches = res.Entity;
            players.BotId = bot.Id;
            await _dataContext.AddAsync(players);
        }
        Console.WriteLine("po pentli");
        await _dataContext.SaveChangesAsync();
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }
    
}