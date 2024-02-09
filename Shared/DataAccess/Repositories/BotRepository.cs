using Microsoft.AspNetCore.Server.Kestrel.Core;
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

    public async Task<HandlerResult<Success, IErrorResult>> addBot(BotDto bot)
    {
        Bot newbot = new Bot()
        {
            PlayerId = bot.PlayerId,
            GameId = bot.GameId,
            BotFile = bot.BotName
        };

        var res =  await _dataContext.Bots.AddAsync(newbot);
        await _dataContext.SaveChangesAsync();
        long botId = res.Entity.Id;

        string filePath = Path.Combine(BotFilePath, botId + Path.GetExtension(bot.file.FileName));
        using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
            await bot.file.CopyToAsync(fileStream);
        }
        return new Success();
    } 
    
}