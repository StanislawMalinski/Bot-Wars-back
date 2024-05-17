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
    private readonly string _gathererEndpointSave = "http://host.docker.internal:7002/api/Gatherer";
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
    
    private async Task GetBotFile(long id,string fileName, long fileId,Language language)
    {
        Console.WriteLine("plik póba");
        var file = await GetFileFromStorage(fileId);
        Console.WriteLine($"plik zdobyt {id}");
        string newBotPath = BotFilePath + "/" + id;
        Directory.CreateDirectory(newBotPath);
        Console.WriteLine("dictioniarty ");
        SaveFileAs(newBotPath,fileName,file,language);
        Console.WriteLine("zapisnaie");
        await Compile(fileName, language, newBotPath);
        Console.WriteLine("compilacja ");
    }
    private async Task GetGameFile(long id,string fileName, long fileId,Language language)
    {
        var file = await GetFileFromStorage(fileId);
        string newGamePath =GameFilePath+ "/" + id;
        Directory.CreateDirectory(newGamePath);
        SaveFileAs(newGamePath,fileName,file,language);
        bool res = await Compile(fileName, language, newGamePath);
    }
    private async Task<string> GetFileFromStorage(long fileId)
    {
        HttpResponseMessage res = await _httpClient.GetAsync(string.Format(_gathererEndpoint, fileId));
        Console.WriteLine("huh");
        if (!res.IsSuccessStatusCode)
        {
            return string.Empty;
        }
        return await res.Content.ReadAsStringAsync();
    }
    private void SaveFileAs( string where,string filename, string fileContent,Language language)
    {
       
        switch (language)
        {
            case Language.C:
                System.IO.File.WriteAllText($"{where}/{ Path.GetFileNameWithoutExtension(filename)}.cpp", fileContent);
                break;
            case Language.PYTHON:
                System.IO.File.WriteAllText($"{where}/{ Path.GetFileNameWithoutExtension(filename)}.py", fileContent);
                break;
            case Language.Java:
                System.IO.File.WriteAllText($"{where}/{ Path.GetFileNameWithoutExtension(filename)}.java", fileContent);
                break;
            
        }
        
    }
    public async Task<string> GetBotFilepath(Bot bot)
    {
        Console.WriteLine($"get Botfile {bot.Id} {bot.FileId}");
        if (Directory.GetDirectories(BotFilePath, bot.Id.ToString()).Length != 0)
        {
            
            string[] dirs;
            switch (bot.Language)
            {
                case Language.C:
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",$"{ Path.GetFileNameWithoutExtension(bot.BotFile)}.out");
                    if (dirs.Length != 0) return dirs[0];
                    break;
                case Language.PYTHON:
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",
                        $"{Path.GetFileNameWithoutExtension(bot.BotFile)}.py");
                    if (dirs.Length != 0) return dirs[0];
                    break;
                case Language.Java:
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",
                        $"{Path.GetFileNameWithoutExtension(bot.BotFile)}.class");
                    if (dirs.Length != 0) return dirs[0];
                    break;
                
            }
        }
        Console.WriteLine($"get Botfile2 {bot.Id} {bot.FileId}");
        await GetBotFile(bot.Id,bot.BotFile, bot.FileId,bot.Language);
        Console.WriteLine($"get Botfil3 {bot.Id} {bot.FileId}");
        switch (bot.Language)
        {
            case Language.C:
                return $"{BotFilePath}/{bot.Id}/{ Path.GetFileNameWithoutExtension(bot.BotFile)}.out";
            case Language.PYTHON:
                return $"{BotFilePath}/{bot.Id}/{Path.GetFileNameWithoutExtension(bot.BotFile)}.py";
            case Language.Java:
                return $"{BotFilePath}/{bot.Id}/{Path.GetFileNameWithoutExtension(bot.BotFile)}.class";
        }
        Console.WriteLine($"get Botfile4 {bot.Id} {bot.FileId}");
        return string.Empty;
    }
    
    public async Task<string> GetGameFilepath(Game game)
    {
        Console.WriteLine($"Get game{game.Id} {game.FileId}");
        if (Directory.GetDirectories(GameFilePath, game.Id.ToString()).Length != 0)
        {
            
            string[] dirs;
            switch (game.Language)
            {
                case Language.C:
                    dirs = Directory.GetFiles($"{GameFilePath}/{game.Id}",$"{ Path.GetFileNameWithoutExtension(game.GameFile)}.out");
                    if (dirs.Length != 0) return dirs[0];
             
                    break;
                case Language.PYTHON:
                    dirs = Directory.GetFiles($"{GameFilePath}/{game.Id}",$"{ Path.GetFileNameWithoutExtension(game.GameFile)}.py");
                    if (dirs.Length != 0) return dirs[0];
                    break;
                case Language.Java:
                    dirs = Directory.GetFiles($"{GameFilePath}/{game.Id}",$"{ Path.GetFileNameWithoutExtension(game.GameFile)}.java");
                    if (dirs.Length != 0) return dirs[0];
                    break;
                
            }
        }
     
        await GetGameFile(game.Id,game.GameFile, game.FileId,game.Language);
     
        switch (game.Language)
        {
            case Language.C:
                return $"{GameFilePath}/{game.Id}/{ Path.GetFileNameWithoutExtension(game.GameFile)}.out";
            case Language.PYTHON:
                return $"{GameFilePath}/{game.Id}/{Path.GetFileNameWithoutExtension(game.GameFile)}.py";
            case Language.Java:
                return $"{GameFilePath}/{game.Id}/{Path.GetFileNameWithoutExtension(game.GameFile)}.class";
        }
        
        return string.Empty;
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

    private async Task<bool> Compile(string fileName, Language language,string where)
    {
        string filepath;
        string compileCommand;
        
        switch (language)
        {
            case (Language.C):
                filepath =$"{where}/{ Path.GetFileNameWithoutExtension(fileName)}.cpp";
                compileCommand = $"g++ -o {where}/{Path.GetFileNameWithoutExtension(fileName)}.out {filepath}"; 
                
                //string compileCommand = "-c g++ -o "+ id+".out "+filepath;
                return await ExecuteCommand(compileCommand);
            case (Language.PYTHON):
                filepath =$"{where}/{ Path.GetFileNameWithoutExtension(fileName)}.py";
                compileCommand = "chmod +x "+filepath;
                return await ExecuteCommand(compileCommand);
            case (Language.Java):
                filepath =$"{where}/{ Path.GetFileNameWithoutExtension(fileName)}.java";
                compileCommand = "javac "+filepath;
                return await ExecuteCommand(compileCommand);
            
        }

        return false;
    }
    

    private async Task<bool> ExecuteCommand(string command)
    {
        Console.WriteLine("compisacja start");
        Process compilerProcess = new Process();
        Console.WriteLine("to cos");
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = $"-c \"{command}\"";;
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.RedirectStandardError = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;
        try
        {
            Console.WriteLine($"rey {command}");
            compilerProcess.Start();
            Console.WriteLine("rey2");
          
            await Task.Delay(1000);
            Console.WriteLine("przcekanie");
            compilerProcess.Kill();
            
            Console.WriteLine("huh");
           
        }
        catch (Exception e)
        {
            Console.WriteLine("zlapanie ");
            return false;
        }
       
        
        return true;
    }
    private static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    public async Task<long> savegameLog(string log,string name)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent =new StreamContent( GenerateStreamFromString(log));
            content.Add(streamContent, "file", name );
            var res = await _httpClient.PutAsync(_gathererEndpointSave, content);
            if (res.IsSuccessStatusCode)
            {
                string cont = await res.Content.ReadAsStringAsync();
                long id = Convert.ToInt32(cont);
                return id;
            }
            else
            {
                return 0;
            }
            
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    
}