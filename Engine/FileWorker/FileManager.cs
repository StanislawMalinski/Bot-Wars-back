using System.Diagnostics;
using System.IO;
using Engine.BusinessLogic.Gameplay;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.FileWorker;

public class FileManager
{
    private string BotFilePath =  "FileSystem/Bots";
    private string GameFilePath = "FileSystem/Games";
    private readonly IFileRepository _fileRepository;
    
    public FileManager(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    // FileDto -> BotFileDto with string FileContent instead of IFormFile file
    public async Task<HandlerResult<SuccessData<FileDto>, IErrorResult>> GetBotFileFromStorage(Bot bot)
    {
        try
        {
            var res = await _fileRepository.GetFile(bot.FileId, bot.BotFile);
            if (!res.IsSuccess)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult",
                    Message = $"File {bot.FileId} not found in Gatherer"
                };
            }
            string? cont = (await _fileRepository.FormFileToString(res.Match(x => x.Data, null))).Match(x => x.Data, null); 
            if (cont == null) 
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult",
                    Message = $"File parsing error"
                };
            }
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
        //await Compile(fileName, language, newBotPath);
        Console.WriteLine("compilacja ");
    }
    private async Task GetGameFile(long id,string fileName, long fileId,Language language)
    {
        var file = await GetFileFromStorage(fileId);
        string newGamePath =GameFilePath+ "/" + id;
        Directory.CreateDirectory(newGamePath);
        SaveFileAs(newGamePath,fileName,file,language);
        //bool res = await Compile(fileName, language, newGamePath);
    }
    private async Task<string> GetFileFromStorage(long fileId)
    {
        var res = await _fileRepository.GetFile(fileId, "file");
        if (!res.IsSuccess)
        {
            throw new ArgumentException("File not found in Gatherer!");
        }
        string? cont = (await _fileRepository.FormFileToString(res.Match(x => x.Data, null))).Match(x => x.Data, null);
        if (cont == null)
        {
            throw new ArgumentException("File is null!");
        }
        Console.WriteLine("huh");
        return cont;
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
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",$"{ Path.GetFileNameWithoutExtension(bot.BotFile)}.cpp");
                    if (dirs.Length != 0) return $"{BotFilePath}/{bot.Id}";
                    break;
                case Language.PYTHON:
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",
                        $"{Path.GetFileNameWithoutExtension(bot.BotFile)}.py");
                    if (dirs.Length != 0) return $"{BotFilePath}/{bot.Id}";
                    break;
                case Language.Java:
                    dirs = Directory.GetFiles($"{BotFilePath}/{bot.Id}",
                        $"{Path.GetFileNameWithoutExtension(bot.BotFile)}.java");
                    if (dirs.Length != 0) return $"{BotFilePath}/{bot.Id}";
                    break;
                
            }
        }
        Console.WriteLine($"get Botfile2 {bot.Id} {bot.FileId}");
        await GetBotFile(bot.Id,bot.BotFile, bot.FileId,bot.Language);
        Console.WriteLine($"get Botfil3 {bot.Id} {bot.FileId}");
        return $"{BotFilePath}/{bot.Id}";
        
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
                    if (dirs.Length != 0) return $"{GameFilePath}/{game.Id}";
             
                    break;
                case Language.PYTHON:
                    dirs = Directory.GetFiles($"{GameFilePath}/{game.Id}",$"{ Path.GetFileNameWithoutExtension(game.GameFile)}.py");
                    if (dirs.Length != 0) return $"{GameFilePath}/{game.Id}";
                    break;
                case Language.Java:
                    dirs = Directory.GetFiles($"{GameFilePath}/{game.Id}",$"{ Path.GetFileNameWithoutExtension(game.GameFile)}.java");
                    if (dirs.Length != 0) return $"{GameFilePath}/{game.Id}";
                    break;
                
            }
        }
     
        await GetGameFile(game.Id,game.GameFile, game.FileId,game.Language);
        return $"{GameFilePath}/{game.Id}";
       
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

   
    

    
    private static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    public async Task<long> SaveGameLog(string log, string name)
    {
        IFormFile file = _fileRepository.StringToFormFile(log, name, "text/plain").Match(x => x.Data, null);
        var res = await _fileRepository.UploadFile(file);
        return res.Match(x => x.Data, x => 0);
    }
    
}