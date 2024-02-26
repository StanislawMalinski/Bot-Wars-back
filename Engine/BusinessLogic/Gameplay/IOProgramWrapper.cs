using System.Diagnostics;
using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.Gameplay;

public class IOProgramWrapper : ICorespondable
{
    private Process? _process;
    private bool isRunning;
    private string _path;
    private StreamWriter sw;
    private StreamReader sr;

    // Program Configuration parameter should be added
    public IOProgramWrapper(string path)
    {
        _path = path;
    }

    // Not safe at all, maybe could be run as user without any privilages
    public async Task<bool> Run()
    {
        if (isRunning)
        {
            throw new Exception("Program already is running");
        }
        isRunning = true;
        _process = new Process();
        Console.WriteLine("-c ./"+_path);
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c ./"+_path,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            UserName = "userexe"
            
        };
        

        _process.StartInfo = startInfo;
        _process.Start();
        sw = _process.StandardInput;
        sr = _process.StandardOutput;
        
        
        /*
        try
        {
            
        }
        catch (Exception ex)
        {
            throw new Exception($"Program cannot be started: {ex.Message}");
        }*/

        return true;
    }

    public async Task<string?> Get()
    {
        
        Task<string> readLineTask = sr.ReadLineAsync();
        Task timeoutTask = Task.Delay(2000); 
        Task completedTask = await Task.WhenAny(readLineTask, timeoutTask);
        if (completedTask == readLineTask)
        {
            // Line was read within the timeout period
            var cos = await readLineTask;
            return cos;
        }
        else
        {
            // Timeout occurred
            Console.WriteLine("Timeout occurred while waiting for a line to be read.");
        }

        throw new Exception("czs się skonczył");
        return String.Empty;
    }

    public async Task Interrupt()
    {
        if (isRunning)
        {
            _process.Kill();
        }

        isRunning = false;

    }

    public async Task<bool> Send(string data)
    {
        if (!isRunning)
        {
            return false;
        }
        Console.WriteLine("piseczos do pliku");
        await sw.WriteLineAsync(data+"\n");
        return true;
    }

    public async Task<string?> SendAndGet(string data)
    {
        await Send(data);
        return await Get();
    }
    private void HandleExit(object? sender, EventArgs e)
    {
        throw new Exception("Program exited");
    }
}