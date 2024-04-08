using System.Diagnostics;
using System.Runtime.InteropServices;
using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.Gameplay;

public class IOProgramWrapper : ICorespondable
{
    private Process? _process;
    private bool isRunning;
    private string _path;
    private StreamWriter sw;
    private StreamReader sr;
    private string BotFilePath =  "FileSystem/Bots";
    private string GameFilePath = "FileSystem/Games";
    private int memorylimit;
    private int timelimit;
    private string cgroupPath;
    private int timemax = 0 ;
    private int memorymax = 0;
    private bool isError = false;

    private ErrorGameStatus _errorType= ErrorGameStatus.InternalError;
    //bash -c "ps -p 119 -o vsz= | grep -o '[0-9]\+'"
    //apt-get install procps
    // Program Configuration parameter should be added
    public IOProgramWrapper(string path,int memorylimit,int timelimit)
    {
        _path = path;
        this.memorylimit = memorylimit;
        this.timelimit = timelimit;
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
       
        
        
        ProcessStartInfo startInfo;
        startInfo = new ProcessStartInfo
        {
            //ulimit -v "+memorylimit+";
            FileName = "bash",
            Arguments = "-c ./"+_path,
            //Arguments = "-c \"(ulimit -v 15000  ; ./"+_path+")\" ",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            UserName = "userexe"

        };


        _process.StartInfo = startInfo;
        try
        {
            _process.Start();
        }
        catch (Exception e)
        {
            isError = true;
            return false;
        }

        sw = _process.StandardInput;
        sr = _process.StandardOutput;
       
        
        return true;
    }

    public async Task<string?> Get()
    {
        try
        {
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Task<string> readLineTask = sr.ReadLineAsync();
            Task timeoutTask = Task.Delay( timelimit); 
            Task completedTask = await Task.WhenAny(readLineTask, timeoutTask);
            milliseconds -= DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (completedTask != readLineTask)
            {
                milliseconds *= -1;
                timemax = Math.Max((int) milliseconds, timemax);
                memorymax = Math.Max(memorymax, GetMemory());
                isError = true;
                _errorType = ErrorGameStatus.TimeLimit;
                return null;
            }
            milliseconds *= -1;
            timemax = Math.Max((int) milliseconds, timemax);
            memorymax = Math.Max(memorymax, GetMemory());
            if (memorymax > memorylimit)
            {
                _errorType = ErrorGameStatus.MemoryLimit;
                return null;
            }
            // Line was read within the timeout period
            var cos = await readLineTask;
            
            return cos;
        }
        catch (Exception e)
        {
            isError = true;
            return null;
        }

    }

    public async Task Interrupt()
    {
        if (isRunning)
        {
            try
            {
                _process.Kill();
                Console.WriteLine("killed "+_process.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("not killed "+_process.Id);
                // ignored
            }
        }
     
        isRunning = false;

    }

    public async Task<bool> Send(string data)
    {
        if (!isRunning)
        {
            return false;
        }

        try
        {
            await sw.WriteLineAsync(data);
        }
        catch (Exception e)
        {
            isError = true;
            _errorType = ErrorGameStatus.InternalError;
            return false;
        }

        return true;
    }

    public async Task<string?> SendAndGet(string data)
    {
        bool res =await Send(data);
        if (res == false) return null;
        return await Get();
    }

    public async Task<string?> getError()
    {
        try
        {
            return await _process.StandardError.ReadToEndAsync();
        }
        catch (Exception e)
        {
            return string.Empty;
        }
       
    }

    public bool wasErros()
    {
        return isError;
    }

    
    public int GetMemory()
    {
        Process process = new Process();
        int pid = _process.Id;
        ProcessStartInfo startInfo;
        startInfo = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c \"ps -p "+pid+" -o vsz= | grep -o '[0-9]\\+'\"",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            UserName = "userexe"
        };
        
        process.StartInfo = startInfo;
      
        process.Start();
        string? memory = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        if (memory == null || string.Empty.Equals(memory))
        {
            return 0;
        }
        return int.Parse(memory);
    }

    public int GetMaxTime()
    {
        return timemax;
    }

    public int GetMaxMemory()
    {
        return memorymax;
    }

    public ErrorGameStatus GetErrorType()
    {
        return _errorType;
    }
}