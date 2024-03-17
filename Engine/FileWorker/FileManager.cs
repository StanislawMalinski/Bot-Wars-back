using System.Diagnostics;
using System.IO;
using Engine.BusinessLogic.Gameplay;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.FileWorker;

public class FileManager
{
    private string BotFilePath =  "FileSystem/Bots";
    private string GameFilePath = "FileSystem/Games";
    private readonly HttpClient _httpClient;
    // move to config
    private readonly string _gathererEndpoint = "http://host.docker.internal:7002/api/Gatherer/{0}";
    public FileManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // FileDto -> BotFileDto with string FileContent instead of IFormFile file
    public async Task<HandlerResult<SuccessData<FileDto>, IErrorResult>> GetBotFileFromStorage(Bot bot)
    {
        try
        {
            Console.WriteLine(string.Format(_gathererEndpoint, bot.FileId));
            HttpResponseMessage res = await _httpClient.GetAsync(string.Format(_gathererEndpoint, bot.FileId));
            if (!res.IsSuccessStatusCode)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult",
                    Message = $"File {bot.FileId} not found in Gatherer"
                };
            }
            string cont = await res.Content.ReadAsStringAsync();

            FileDto ret = new FileDto()
            {
                PlayerId = bot.PlayerId,
                GameId = bot.GameId,
                BotId = bot.Id,
                BotName = bot.BotFile,
                FileContent = cont
            };
            return new SuccessData<FileDto>() { Data = ret };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new EntityNotFoundErrorResult
            {
                Title = "Exception",
                Message = ex.Message
            };
        }
    }

    public async Task<HandlerResult<Success,IErrorResult>> addbot(BotFileDto botFileDto)
    {
        long id = botFileDto.BotId;
        var fres = await SaveFile(botFileDto.file,id , BotFilePath);
        if (!fres)
        {
            return new NotImplementedError()
            {
                Message = "bląd kopilcji"
            };
        }

        var res = await Compile(id, Language.C, BotFilePath);
        Console.WriteLine("compie "+ res);
        if (!res)
        {
            return new NotImplementedError()
            {
                Message = "bląd kopilcji"
            };
        }
        return new Success();

    }
    public async Task<HandlerResult<Success,IErrorResult>> addGame(GameFileDto gameFileDto){
        
        long id = gameFileDto.GameId;
        var fres = await SaveFile(gameFileDto.file,id , GameFilePath);
        if (!fres)
        {
            return new NotImplementedError()
            {
                Message = "plik się nie zapisał"
            };
        }

        var res = await Compile(id, Language.C, GameFilePath);
       
        if (!res)
        {
            return new NotImplementedError()
            {
                Message = "bląd kopilcji"
            };
        }
        return new Success();
    }
    
    public string GetBotFilepath(long id)
    {
        string[] dirs = Directory.GetFiles(BotFilePath, id + ".*");
        foreach(string s in dirs)
        {
            if (Path.GetExtension(s) == ".out")
            {
                return s;
            }
        }
        return string.Empty;
    }
    
    public string GetGameFilepath(long id)
    {
        string[] dirs = Directory.GetFiles(GameFilePath, id + ".*");
        foreach(string s in dirs)
        {
            if (Path.GetExtension(s) == ".out")
            {
                return s;
            }
        }
        return string.Empty;
    }

    public async Task<bool> TestSaveFile(IFormFile file, long id, string where)
    {
        return await SaveFile(file, id, where);
    }

    private async Task<bool> SaveFile(IFormFile file,long id,string where)
    {
        string filePath = where + "/"+ id + Path.GetExtension(file.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
            await file.CopyToAsync(fileStream);
        }

        return true;
    }

    private async Task<bool> Compile(long id, Language language,string where)
    {
        switch (language)
        {
            case (Language.C):
                string filepath = where + "/"+  id + ".cpp";
                string compileCommand = "g++ -o "+ where + "/"+id+".out "+filepath;
                
                //string compileCommand = "-c g++ -o "+ id+".out "+filepath;
                return await ExecuteCommand(compileCommand);
        }

        return false;
    }
    

    private async Task<bool> ExecuteCommand(string command)
    {
        Process compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = $"-c \"{command}\"";;
        Console.WriteLine(command);
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.RedirectStandardError = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;
        
        compilerProcess.Start();
        string output = compilerProcess.StandardOutput.ReadToEnd();
        string error = compilerProcess.StandardError.ReadToEnd();
        compilerProcess.WaitForExit();
        Console.WriteLine(output);
        if (compilerProcess.ExitCode != 0)
        {
            
            Console.WriteLine(error);
            return false;
        }
        
        return true;
    }

    public async Task<bool> bottest()
    {
        Console.WriteLine(GetBotFilepath(4));
        IOProgramWrapper p4 = new IOProgramWrapper(GetBotFilepath(4));
        await p4.Run();
        await p4.Send("0 0 0 1");
        Console.WriteLine("a");
        var res = await p4.Get();
        Console.WriteLine(res);
        await p4.Send("1");
        res = await p4.Get();
        Console.WriteLine(res);
        p4.Interrupt();
        /*
       
       
        Console.WriteLine(await  p4.Get());
        await p4.Send("1");
        Console.WriteLine(await  p4.Get());
        await p4.Send("1");
        Console.WriteLine(await  p4.Get());
        */
        return true;
    }
}