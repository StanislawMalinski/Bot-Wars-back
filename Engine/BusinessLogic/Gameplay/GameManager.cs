﻿using Engine.BusinessLogic.Gameplay.Interface;
using Engine.FileWorker;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.Gameplay;

public class GameManager : IGameManager
{
    private IOProgramWrapper[] bots;
    private Bot[] botsArray;

    public async Task<GameResult> PlayGame(Game gameData, List<Bot> botsData, int memoryLimit = 1073741824,
        int timeLimit = 2000)
    {
        Console.WriteLine("play game ");
        botsArray = botsData.ToArray();
        var manager = new FileManager(new FileRepository(new HttpClient()));
        bots = new IOProgramWrapper[botsData.Count()];
        Console.WriteLine("play wrapery ");

        var game = new IOProgramWrapper(await manager.GetGameFilepath(gameData), gameData.GameFile!, memoryLimit,
            timeLimit, gameData.Language);
        var ind = 0;
        Console.WriteLine("hejo");
        foreach (var bot in botsArray)
        {
            Console.WriteLine("bots playa");
            bots[ind] = new IOProgramWrapper(await manager.GetBotFilepath(bot), bot.BotFile, memoryLimit, timeLimit,
                bot.Language);
            Console.WriteLine("bot id " + bot.Id);

            await bots[ind].Run();
            ind++;
        }

        Console.WriteLine("bots play");
        ind = 0;
        var gamelog = string.Empty;
        foreach (var bot in botsArray)
        {
            if (bots[ind].wasErros())
            {
                Console.WriteLine("przerwanie botow");
                await InterruptAllBots();
                Console.WriteLine("porawnie przerwanoboty");
                gamelog += bots[ind].GetErrorType().ToString() + '\n';
                return new ErrorGameResult
                {
                    gameLog = gamelog,
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType()
                };
            }

            ind++;
        }

        var ok = true;
        Console.WriteLine("play game run ");
        await game.Run();
        Console.WriteLine("ropoczęcie ");
        var curr = await game.Get();
        if (game.wasErros())
        {
            gamelog = "game error";
            gamelog += game.GetErrorType().ToString() + '\n';
            return new ErrorGameResult
            {
                gameLog = gamelog,
                BotError = false,
                GameError = true,
                ErrorGameStatus = game.GetErrorType()
            };
        }

        var nextBot = 0;
        var counter = 0;
        var counterMax = 10000;

        gamelog += curr + '\n';
        Console.WriteLine($"sprawdznie przed {bots[nextBot].wasErros()}");
        while (int.Parse(curr) != -1 && counter < counterMax)
        {
            nextBot = int.Parse(curr);
            curr = await game.Get();
            if (curr == null)
            {
                ok = false;
                Console.WriteLine(" to eror 1 " + bots[nextBot].wasErros());
                break;
            }

            gamelog += curr + '\n';
            curr = await bots[nextBot].SendAndGet(curr);
            if (curr == null)
            {
                ok = false;
                Console.WriteLine(" to eror 2 " + bots[nextBot].wasErros());
                break;
            }
            
            gamelog += curr + '\n';
            curr = await game.SendAndGet(curr);
            if (curr == null)
            {
                ok = false;
                Console.WriteLine(" to eror 3 " + bots[nextBot].wasErros());
                break;
            }

            gamelog += curr + '\n';
            counter++;
        }

        Console.WriteLine(bots[0].GetErrorType().ToString());

        if (ok)
        {
            if (counter < counterMax)
            {
                curr = await game.Get();
                if (curr == null)
                {
                    await game.Interrupt();
                    await InterruptAllBots();
                    gamelog += game.GetErrorType().ToString() + '\n';
                    return new ErrorGameResult
                    {
                        gameLog = gamelog,
                        BotError = false,
                        GameError = true,
                        ErrorGameStatus = game.GetErrorType()
                    };
                }

                gamelog += curr + '\n';
                nextBot = int.Parse(curr);
                Console.WriteLine(curr + " to jest zwyczezca");
                //winner
            }
            else
            {
                gamelog += "game take to long \n";
                nextBot = 0;
            }

            Console.WriteLine(nextBot);
            Console.WriteLine("jest zwyciezca");
            var cos = botsArray[nextBot];
            await game.Interrupt();
            await InterruptAllBots();

            return new SuccessfullGameResult
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
            gamelog += game.GetErrorType().ToString() + '\n';

            return new ErrorGameResult
            {
                GameError = true,
                BotError = true,
                BotErrorId = botsArray[nextBot].Id,
                ErrorGameStatus = game.GetErrorType(),
                gameLog = gamelog
            };
        }

        ind = 0;
        foreach (var bot in botsArray)
        {
            Console.WriteLine("bot was eeroe");
            if (bots[ind].wasErros())
            {
                Console.WriteLine("bot was znaleziony " + ind);
                gamelog += bots[ind].GetErrorType().ToString() + '\n';

                return new ErrorGameResult
                {
                    BotError = true,
                    BotErrorId = bot.Id,
                    ErrorGameStatus = bots[ind].GetErrorType(), gameLog = gamelog
                };
            }

            ind++;
        }

        Console.WriteLine("niemozliwey was eeroe");
        return new ErrorGameResult
        {
            gameLog = gamelog
        };
    }

    private async Task InterruptAllBots()
    {
        foreach (var bot in bots) await bot.Interrupt();
    }

    public BotsPerformers[] GetBotsPerformers()
    {
        var result = new BotsPerformers[bots.Length];
        var ind = 0;
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