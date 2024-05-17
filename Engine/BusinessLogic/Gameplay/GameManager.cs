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
        Console.WriteLine("play game ");
        botsArray = botsData.ToArray();
        FileManager manager = new FileManager(new HttpClient()); //???
        bots = new IOProgramWrapper[botsData.Count()];
        Console.WriteLine("play wrapery ");
        IOProgramWrapper game = new IOProgramWrapper(await manager.GetGameFilepath(gameData),memoryLimit,timeLimit,gameData.Language);
        int ind = 0;
        Console.WriteLine("hejo");
        foreach (var bot in botsArray)
        {
            Console.WriteLine("bots playa");
            bots[ind] = new IOProgramWrapper( await manager.GetBotFilepath(bot),memoryLimit,timeLimit,bot.Language);
            Console.WriteLine("bot id "+bot.Id);
            await bots[ind].Run();
            ind++;
        }
        Console.WriteLine("bots play");
        ind = 0;
        string gamelog = string.Empty;
        foreach (var bot in botsArray)
        {
            if (bots[ind].wasErros())
            {
                await InterruptAllBots();
                return new ErrorGameResult()
                {
                    gameLog = gamelog,
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType()
                };
            }
            ind++;
        }
        
        bool ok = true;
        Console.WriteLine("play game run ");
        await game.Run();
        Console.WriteLine("ropoczęcie ");
        string? curr = await game.Get();
        if (game.wasErros())
        {
            gamelog = "game error";
            return new ErrorGameResult
            {
                gameLog = gamelog,
                BotError = false,
                GameError = true,
                ErrorGameStatus = game.GetErrorType()
            };
        }
        int nextBot = 0;
        int counter = 0;
        int counterMax = 1000;
        
        gamelog += curr+'\n';
        
        while (Int32.Parse(curr) != -1 && counter < counterMax)
        {
            nextBot = Int32.Parse(curr);
            curr = await game.Get();
            if (curr == null) {ok = false;  Console.WriteLine(" to eror 1 "+bots[nextBot].wasErros()); break;}
            gamelog += curr+'\n';
            curr = await bots[nextBot].SendAndGet(curr);
            if(curr == null) {ok = false;  Console.WriteLine(" to eror 2 "+bots[nextBot].wasErros()); break;}
            gamelog += curr+'\n';
            curr = await game.SendAndGet(curr);
            if(curr == null) {ok = false;  Console.WriteLine(" to eror 3 "+bots[nextBot].wasErros()); break;}
            gamelog += curr+'\n';
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
                        gameLog = gamelog,
                        BotError = false,
                        GameError = true,
                        ErrorGameStatus = game.GetErrorType()
                    };
                }

                gamelog += curr+'\n';
                nextBot = Int32.Parse(curr);
                Console.WriteLine(curr + " to jest zwyczezca");
                //winner

            }
            else
            {
                nextBot = 0;
            }
            
            Console.WriteLine(nextBot);
            Console.WriteLine("jest zwyciezca");
            var cos = botsArray[nextBot];
            await game.Interrupt();
            await InterruptAllBots();
            
            return new SuccessfullGameResult()
            {
                gameLog = gamelog,
                BotWinner = cos
            };
        }
        Console.WriteLine("game in eerrror game manegar");
        await game.Interrupt();
        await InterruptAllBots();
        if (game.wasErros())
        {
            Console.WriteLine("game was eeror");
            return new ErrorGameResult()
            {
                GameError = true,
                BotError = true,
                BotErrorId = botsArray[nextBot].Id,
                ErrorGameStatus = game.GetErrorType()
            };
        
        }

        ind = 0;
        foreach (var bot in botsArray)
        {
            Console.WriteLine("bot was eeroe");
            if (bots[ind].wasErros())
            {
                Console.WriteLine("bot was znaleziony "+ind);
                return new ErrorGameResult()
                {
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType()
                };
            }
            ind++;
        }
        Console.WriteLine("niemozliwey was eeroe");
        return new ErrorGameResult()
        {
            gameLog = gamelog,
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