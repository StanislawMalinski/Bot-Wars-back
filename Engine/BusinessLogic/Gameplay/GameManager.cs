using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using Engine.BusinessLogic.Gameplay.Interface;
using Engine.FileWorker;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay;

public class GameManager : IGameManager
{
    public async Task<GameResult> PlayGame(Game gameData, List<Bot> botsData)
    {
        
        
        return new SuccessfullGameResult()
        {
            BotWinner = botsData[0],
            BotLoser = botsData[1],
        };
        var botsArray = botsData.ToArray();
        FileManager manager = new FileManager(); //?
        IOProgramWrapper[] bots = new IOProgramWrapper[botsData.Count()];
        IOProgramWrapper game = new IOProgramWrapper(manager.GetGameFilepath(gameData.Id));
        int ind = 0;
        foreach (var bot in botsArray)
        {
            
            bots[ind] = new IOProgramWrapper(manager.GetBotFilepath(bot.Id));
            await bots[ind].Run();
            ind++;
        }

        await game.Run();
    
        string curr = await game.Get();
        int nextBot;
        while (Int32.Parse(curr)  != -1)
        {
            nextBot = Int32.Parse(curr);
            curr = await game.Get();
            await bots[nextBot].Send(curr);
            curr = await bots[nextBot].Get();
            await game.Send(curr);
            curr = await game.Get();
            
        }
        curr = await game.Get();
        nextBot = Int32.Parse(curr);
        //winner
        var cos =  botsArray[nextBot].Id;
    }
}