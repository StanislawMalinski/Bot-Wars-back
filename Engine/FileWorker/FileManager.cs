using System.Diagnostics;
using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.FileWorker;

public class FileManager
{
    private string BotFilePath =  "FileSystem/Bots";
    private string GameFilePath = "FileSystem/Games";
    public FileManager()
    {
        
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

        Compile(id, Language.C, BotFilePath);
        return new Success();

    }
    public async Task<HandlerResult<Success,IErrorResult>> addGame(GameFileDto gameFileDto){
        
        long id = gameFileDto.GameId;
        var fres = await SaveFile(gameFileDto.file,id , BotFilePath);
        if (!fres)
        {
            return new NotImplementedError()
            {
                Message = "plik się nie zapisał"
            };
        }

        if (!Compile(id, Language.C, BotFilePath))
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
            if (Path.GetExtension(s) == ".cpp")
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
            if (Path.GetExtension(s) == ".cpp")
            {
                return s;
            }
        }
        return string.Empty;
    }

    private async Task<bool> SaveFile(IFormFile file,long id,string where)
    {
        string filePath = where + "/"+ id + Path.GetExtension(file.FileName);
        using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
            await file.CopyToAsync(fileStream);
        }

        return true;
    }
    private bool Compile(long id, Language language,string where)
    {
        switch (language)
        {
            case (Language.C):
                string filepath = where + "/"+  id + ".cpp";
                //string compileCommand = "-c g++ -o "+ where + "/"+id+".out "+filepath;
                string compileCommand = "-c g++ -o output.exe "+filepath;
                
                //string compileCommand = "-c g++ -o "+ id+".out "+filepath;
                return ExecuteCommand(compileCommand);
        }

        return false;
    }
    

    private bool ExecuteCommand(string command)
    {
        Process compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = command;
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
}