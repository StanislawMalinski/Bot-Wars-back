using System.Diagnostics;
using System.IO;
using Engine.BusinessLogic.Gameplay;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
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
        var fres = await SaveFile(botFileDto.file,id , BotFilePath,Language.C);
        if (!fres)
        {
            return new NotImplementedError()
            {
                Message = "bląd kopilcji"
            };
        }

        var res = await Compile(id, Language.C, BotFilePath);
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
        var fres = await SaveFile(gameFileDto.file,id , GameFilePath, Language.C);
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

    private async Task getBotFile(long id, long fileId,Language language)
    {
        var file = await GetFileFromStorage(fileId);
        
        SaveFileAs(BotFilePath,id,file,language);
        await Compile(id, Language.C, BotFilePath);
    }
    private async Task getGameFile(long id, long fileId,Language language)
    {
        var file = await GetFileFromStorage(fileId);
        SaveFileAs(GameFilePath,id,file,language);
        bool res = await Compile(id, Language.C, GameFilePath);
    }
    private async Task<string> GetFileFromStorage(long fileId)
    {
        HttpResponseMessage res = await _httpClient.GetAsync(string.Format(_gathererEndpoint, fileId));
        if (!res.IsSuccessStatusCode)
        {
            return string.Empty;
        }
        return await res.Content.ReadAsStringAsync();
    }
    private void SaveFileAs( string where,long id, string fileContent,Language language)
    {
        switch (language)
        {
            case Language.C:
                System.IO.File.WriteAllText($"{where}/{id}.cpp", fileContent);
                break;
            case Language.PYTHON:
                System.IO.File.WriteAllText($"{where}/{id}.py", fileContent);
                break;
            
        }
        
    }
    public async Task<string> GetBotFilepath(Bot bot)
    {
        string[] dirs = Directory.GetFiles(BotFilePath, bot.Id + ".*");
        foreach(string s in dirs)
        {
            if (Path.GetExtension(s) == ".out")
            {
                return s;
            }
        }
        await getBotFile(bot.Id,bot.FileId,bot.Language);
        switch (bot.Language)
        {
            case Language.C:
                return $"{BotFilePath}/{bot.Id}.out";
            case Language.PYTHON:
                return $"{BotFilePath}/{bot.Id}.py";
        }
        
        return string.Empty;
    }
    
    public async Task<string> GetGameFilepath(Game game)
    {
        string[] dirs = Directory.GetFiles(GameFilePath, game.Id + ".*");
        foreach(string s in dirs)
        {
            if (Path.GetExtension(s) == ".out")
            {
                return s;
            }
        }
        await getGameFile(game.Id,game.FileId,game.Language);
        /*switch ()
        {
            
        }*/ ;
       

        return $"{GameFilePath}/{game.Id}.out";
    }

    public async Task<bool> TestSaveFile(IFormFile file, long id, string where)
    {
        return await SaveFile(file, id, where,Language.C);
    }

    private async Task<bool> SaveFile(IFormFile file,long id,string where,Language language)
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
            case (Language.PYTHON):
                return true;
        }

        return false;
    }
    

    private async Task<bool> ExecuteCommand(string command)
    {
        Process compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = $"-c \"{command}\"";;
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.RedirectStandardError = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;
        
        compilerProcess.Start();
        string output = compilerProcess.StandardOutput.ReadToEnd();
        string error = compilerProcess.StandardError.ReadToEnd();
        compilerProcess.WaitForExit();
        if (compilerProcess.ExitCode != 0)
        {
            
            return false;
        }
        
        return true;
    }
    
}