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
        
        
        /*return new SuccessfullGameResult()
        {
            BotWinner = botsData[0],
            BotLoser = botsData[1],
        };*/
        var botsArray = botsData.ToArray();
        FileManager manager = new FileManager(new HttpClient()); //???
        IOProgramWrapper[] bots = new IOProgramWrapper[botsData.Count()];
        IOProgramWrapper game = new IOProgramWrapper(await manager.GetGameFilepath(gameData));
        int ind = 0;
        Console.WriteLine("gry gotowe");
        foreach (var bot in botsArray)
        {
            
            bots[ind] = new IOProgramWrapper( await manager.GetBotFilepath(bot));
            await bots[ind].Run();
            ind++;
        }

        await game.Run();
        Console.WriteLine("ropoczęcie ");
        string curr = await game.Get();
        int nextBot;
        int counter = 0;
        int counterMax = 100000;
        string gamelog = string.Empty;
        gamelog += curr;
        while (Int32.Parse(curr)  != -1 && counter < counterMax)
        {
            nextBot = Int32.Parse(curr);
            curr = await game.Get();
            gamelog += curr;
            await bots[nextBot].Send(curr);
            curr = await bots[nextBot].Get();
            gamelog += curr;
            await game.Send(curr);
            curr = await game.Get();
            gamelog += curr;
            counter++;
         
        }
        if (counter < counterMax)
        {
            curr = await game.Get();
            gamelog += curr;
            nextBot = Int32.Parse(curr);
            Console.WriteLine(curr+ " to jest zwyczezca");
            //winner
           
        }
        else
        {
            nextBot = 0;
        }
        Console.WriteLine(gamelog);
        Console.WriteLine(nextBot);
        Console.WriteLine("jest zwyciezca");
        var cos =  botsArray[nextBot];
        return new SuccessfullGameResult()
        {
            BotWinner = cos
        };
    }
}