using System.Diagnostics;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class BotRepository
{
    private DataContext _dataContext;
    private string BotFilePath = "FileSystem/Bots";

    public BotRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> addBot(BotFileDto bot)
    {
        Bot newbot = new Bot()
        {
            PlayerId = bot.PlayerId,
            GameId = bot.GameId,
            BotFile = bot.BotName
        };
        
        var res =  await _dataContext.Bots.AddAsync(newbot);
        await _dataContext.SaveChangesAsync();
        
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }

   

}