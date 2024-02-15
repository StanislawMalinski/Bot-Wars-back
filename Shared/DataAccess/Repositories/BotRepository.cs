using System.Diagnostics;
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
        Console.WriteLine("dsaa");
        string filePath = BotFilePath + "/"+ botId + Path.GetExtension(bot.file.FileName);
        Console.WriteLine(filePath);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
            await bot.file.CopyToAsync(fileStream);
        }
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> compile(long id)
    {
        string filepath = BotFilePath + "/"+  id + ".cpp";
        Console.WriteLine(filepath);
        /*
        // Compiler command (change this according to your compiler)
        string compilerCommand = $"g++ -o \"{BotFilePath+"/" +id+".cpp"}\" \"{BotFilePath+"/" +id+".exe"}\"";

        // Create a process to run the compiler
        Process compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "../usr/bin/gcc";
        compilerProcess.StartInfo.RedirectStandardInput = true;
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;

        // Start the process
        compilerProcess.Start();

        // Pass the compiler command to the process
        compilerProcess.StandardInput.WriteLine(compilerCommand);
        compilerProcess.StandardInput.Flush();
        compilerProcess.StandardInput.Close();

        // Wait for the compilation to finish
        compilerProcess.WaitForExit();

        // Close the process
        compilerProcess.Close();

        // Check if the compilation was successful
        if (true)
        {
            Console.WriteLine("Compilation successful!");
        }
        else
        {
            Console.WriteLine("Compilation failed!");
        }*/
        
        string compileCommand = "g++ -o output.exe "+filepath;

        // Create a process to run the compiler command
        Process compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = $"-c \"{compileCommand}\"";
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.RedirectStandardError = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;

        // Start the process
        compilerProcess.Start();

        // Read compiler output
        string output = compilerProcess.StandardOutput.ReadToEnd();
        string error = compilerProcess.StandardError.ReadToEnd();

        // Wait for the process to exit
        compilerProcess.WaitForExit();

        // Print output and error messages
        Console.WriteLine("Compiler Output:");
        Console.WriteLine(output);
        Console.WriteLine("Compiler Errors:");
        Console.WriteLine(error);

        // Check if compilation was successful
        if (compilerProcess.ExitCode == 0)
        {
            Console.WriteLine("Compilation successful!");
        }
        else
        {
            Console.WriteLine("Compilation failed!");
        }
        
        
        return new Success();
    }

}