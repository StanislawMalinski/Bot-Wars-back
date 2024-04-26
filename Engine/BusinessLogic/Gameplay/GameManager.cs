using System.Collections;
using System.Runtime.InteropServices.JavaScript;
using Engine.BusinessLogic.Gameplay.Interface;
using Engine.FileWorker;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;

namespace Engine.BusinessLogic.Gameplay;

public class GameManager : IGameManager
{
    private IOProgramWrapper[] bots;
    private Bot[] botsArray;
    public async Task<GameResult> PlayGame(Game gameData, List<Bot> botsData,int memoryLimit = 1073741824, int timeLimit=2000)
    {
        
        botsArray = botsData.ToArray();
        FileManager manager = new FileManager(new HttpClient()); //???
        bots = new IOProgramWrapper[botsData.Count()];
        IOProgramWrapper game = new IOProgramWrapper(await manager.GetGameFilepath(gameData),memoryLimit,timeLimit,Language.C);
        int ind = 0;
        
        foreach (var bot in botsArray)
        {
            bots[ind] = new IOProgramWrapper( await manager.GetBotFilepath(bot),memoryLimit,timeLimit,bot.Language);
            await bots[ind].Run();
            ind++;
        }

        ind = 0;
        foreach (var bot in botsArray)
        {
            if (bots[ind].wasErros())
            {
                await InterruptAllBots();
                return new ErrorGameResult()
                {
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType()
                };
            }
            ind++;
        }
        
        
        bool ok = true;
        await game.Run();
        Console.WriteLine("ropoczęcie ");
        string? curr = await game.Get();
        if (game.wasErros())
        {
            return new ErrorGameResult
            {
                BotError = false,
                GameError = true,
                ErrorGameStatus = game.GetErrorType()
            };
        }
        int nextBot = 0;
        int counter = 0;
        int counterMax = 100000;
        string gamelog = string.Empty;
        gamelog += curr;
        while (Int32.Parse(curr) != -1 && counter < counterMax)
        {
            nextBot = Int32.Parse(curr);
            curr = await game.Get();
            if (curr == null) {ok = false;break;}
            gamelog += curr;
            curr = await bots[nextBot].SendAndGet(curr);
            if(curr == null) {ok = false;break;}
            gamelog += curr;
            curr = await game.SendAndGet(curr);
            if(curr == null) {ok = false;break;}
            gamelog += curr;
            counter++;

        }

        if (ok)
        {
            if (counter < counterMax)
            {
                curr = await game.Get();
                if (curr == null)
                {
                    await game.Interrupt();
                    await InterruptAllBots();
                    return new ErrorGameResult
                    {
                        BotError = false,
                        GameError = true,
                        ErrorGameStatus = game.GetErrorType()
                    };
                }

                gamelog += curr;
                nextBot = Int32.Parse(curr);
                Console.WriteLine(curr + " to jest zwyczezca");
                //winner

            }
            else
            {
                nextBot = 0;
            }

            Console.WriteLine(gamelog);
            Console.WriteLine(nextBot);
            Console.WriteLine("jest zwyciezca");
            var cos = botsArray[nextBot];
            await game.Interrupt();
            await InterruptAllBots();

            return new SuccessfullGameResult()
            {
                BotWinner = cos
            };
        }

        await game.Interrupt();
        await InterruptAllBots();
        if (game.wasErros())
        {
            return new ErrorGameResult()
            {
                GameError = true,
                BotError = true,
                BotErrorId = botsArray[nextBot].Id,
                ErrorGameStatus = game.GetErrorType()
            };
        }
        foreach (var bot in botsArray)
        {
            if (bots[ind].wasErros())
            {
                return new ErrorGameResult()
                {
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType()
                };
            }
            ind++;
        }
        
        return new ErrorGameResult()
        {
            
        };
        
    }
    private async Task InterruptAllBots()
    {
        foreach (var bot in bots)
        {
            await bot.Interrupt();
        }
    }

    public BotsPerformers[] GetBotsPerformers()
    {
        BotsPerformers[] result = new BotsPerformers[bots.Length];
        int ind = 0;
        foreach (var bot in botsArray)
        {
            result[ind] = new BotsPerformers
            {
                MemoryUse = bots[ind].GetMaxMemory(),
                TimeUse = bots[ind].GetMaxTime(),
                BotId = bot.Id
            };
            ind++;
        }

        return result;
    }
   
}