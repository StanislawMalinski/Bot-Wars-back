using System.Diagnostics;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.Enumerations;

namespace Engine.BusinessLogic.Gameplay;

public class IOProgramWrapper : ICorespondable
{
    private ErrorGameStatus _errorType = ErrorGameStatus.InternalError;
    private string _exePath;
    private readonly string _fileName;
    private readonly Language _language;
    private readonly string _path;
    private Process? _process;
    private string BotFilePath = "FileSystem/Bots";
    private string cgroupPath;
    private string GameFilePath = "FileSystem/Games";
    private bool isError;
    private bool isRunning;
    private readonly int memorylimit;
    private int memorymax;
    private StreamReader sr;
    private StreamWriter sw;
    private readonly int timelimit;

    private int timemax;

    //bash -c "ps -p 119 -o vsz= | grep -o '[0-9]\+'"
    //apt-get install procps
    // Program Configuration parameter should be added
    public IOProgramWrapper(string path, string fileName, int memorylimit, int timelimit, Language language)
    {
        _path = path;
        _fileName = fileName;
        Console.WriteLine(_path + " siceiszka1");
        this.memorylimit = memorylimit*1000;
        this.timelimit = timelimit;
        _language = language;
    }

    // Not safe at all, maybe could be run as user without any privilages
    public async Task<bool> Run()
    {
        Console.WriteLine("uruchominei programy");
        if (isRunning) return false;
        isRunning = true;
        _process = new Process();

        var res = await Compile();
        Console.WriteLine("po kompilacji");
        if (res == false)
        {
            Console.WriteLine("niew udana compilacja");
            isError = true;
            return false;
        }

        Console.WriteLine("exe -" + _exePath + " siceiszka");
        ProcessStartInfo startInfo;
        switch (_language)
        {
            case Language.C:
                startInfo = new ProcessStartInfo
                {
                    //ulimit -v "+memorylimit+";
                    FileName = "bash",
                    Arguments = "-c ./" + _exePath,
                    //Arguments = "-c \"(ulimit -v 15000  ; ./"+_path+")\" ",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    UserName = "userexe"
                };
                break;
            case Language.PYTHON:
                startInfo = new ProcessStartInfo
                {
                    //ulimit -v "+memorylimit+";
                    FileName = "bash",
                    Arguments = "-c \"python3 " + _exePath + "\"",
                    //Arguments = "-c \"(ulimit -v 15000  ; ./"+_path+")\" ",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    UserName = "userexe"
                };
                break;
            case Language.Java:
                Console.WriteLine($"-c \"java -cp {_path} {_exePath}\"");
                startInfo = new ProcessStartInfo
                {
                    //ulimit -v "+memorylimit+";
                    FileName = "bash",
                    Arguments = $"-c \"java -cp {_path} {_exePath}\"",
                    //Arguments = "-c \"(ulimit -v 15000  ; ./"+_path+")\" ",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    UserName = "userexe"
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        _process.StartInfo = startInfo;
        try
        {
            _process.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine("game worker innit start  err");
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
            var milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Task<string> readLineTask = sr.ReadLineAsync();
            var timeoutTask = Task.Delay(timelimit);
            var completedTask = await Task.WhenAny(readLineTask, timeoutTask);
            milliseconds -= DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (completedTask != readLineTask)
            {
                milliseconds *= -1;
                timemax = Math.Max((int)milliseconds, timemax);
                memorymax = Math.Max(memorymax, GetMemory());
                isError = true;
                _errorType = ErrorGameStatus.TimeLimit;
                Console.WriteLine("hej??? e1 ");
                return null;
            }

            milliseconds *= -1;
            timemax = Math.Max((int)milliseconds, timemax);
            memorymax = Math.Max(memorymax, GetMemory());
            if (memorymax > memorylimit)
            {
                Console.WriteLine("hej??? e2 ");
                _errorType = ErrorGameStatus.MemoryLimit;
                isError = true;
                return null;
            }

            var cos = await readLineTask;
            if (cos == null)
            {
                Console.WriteLine("hej??? ");
                isError = true;
            }

            return cos;
        }
        catch (Exception e)
        {
            Console.WriteLine("game worker innit get eeror ");
            isError = true;
            return null;
        }
    }

    public async Task Interrupt()
    {
        if (isRunning)
            try
            {
                if (_process != null)
                {
                    _process.Kill();
                    Console.WriteLine("killed " + _process.Id);
                }
            }
            catch (Exception e)
            {
                isError = true;
            }

        isRunning = false;
    }

    public async Task<bool> Send(string data)
    {
        if (!isRunning)
        {
            isError = true;
            return false;
        }

        try
        {
            await sw.WriteLineAsync(data);
        }
        catch (Exception e)
        {
            Console.WriteLine("game worker innit asdas");
            isError = true;
            _errorType = ErrorGameStatus.InternalError;
            return false;
        }

        return true;
    }

    public async Task<string?> SendAndGet(string data)
    {
        var res = await Send(data);
        if (res == false) return null;
        return await Get();
    }

    private async Task<bool> Compile()
    {
        try
        {
            var res = await Compile(_fileName, _language, _path);
            string[] dirs;
            switch (_language)
            {
                case Language.C:
                    dirs = Directory.GetFiles($"{_path}", $"{Path.GetFileNameWithoutExtension(_fileName)}.out");
                    if (dirs.Length != 0) _exePath = dirs[0];

                    break;
                case Language.PYTHON:
                    dirs = Directory.GetFiles($"{_path}", $"{Path.GetFileNameWithoutExtension(_fileName)}.py");
                    if (dirs.Length != 0) _exePath = dirs[0];
                    break;
                case Language.Java:
                    dirs = Directory.GetFiles($"{_path}", $"{Path.GetFileNameWithoutExtension(_fileName)}.class");
                    if (dirs.Length != 0) _exePath = $"{Path.GetFileNameWithoutExtension(_fileName)}";
                    break;
            }

            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine("zlapanie ");
            return false;
        }
    }

    public async Task<string?> getError()
    {
        try
        {
            return await _process.StandardError.ReadToEndAsync();
        }
        catch (Exception e)
        {
            isError = true;
            Console.WriteLine("game worker i geteerror reerro ");
            return string.Empty;
        }
    }

    public bool wasErros()
    {
        return isError;
    }


    public int GetMemory()
    {
        var process = new Process();
        var pid = _process.Id;
        ProcessStartInfo startInfo;
        startInfo = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c \"ps -p " + pid + " -o vsz= | grep -o '[0-9]\\+'\"",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            UserName = "userexe"
        };

        process.StartInfo = startInfo;

        process.Start();
        var memory = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        if (memory == null || string.Empty.Equals(memory)) return 0;
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


    private async Task<bool> Compile(string fileName, Language language, string where)
    {
        string filepath;
        string compileCommand;
        Console.WriteLine("pryba kompilacji");

        switch (language)
        {
            case Language.C:
                filepath = $"{where}/{Path.GetFileNameWithoutExtension(fileName)}.cpp";
                compileCommand = $"g++ -o {where}/{Path.GetFileNameWithoutExtension(fileName)}.out {filepath}";

                //string compileCommand = "-c g++ -o "+ id+".out "+filepath;
                return await ExecuteCommand(compileCommand);
            case Language.PYTHON:
                //filepath =$"{where}/{ Path.GetFileNameWithoutExtension(fileName)}.py";
                //compileCommand = "chmod +x "+filepath;
                //return await ExecuteCommand(compileCommand);
                return true;
            case Language.Java:
                filepath = $"{where}/{Path.GetFileNameWithoutExtension(fileName)}.java";
                compileCommand = "javac " + filepath;
                return await ExecuteCommand(compileCommand);
        }


        return false;
    }

    private async Task<bool> ExecuteCommand(string command)
    {
        Console.WriteLine("compisacja start");
        var compilerProcess = new Process();
        compilerProcess.StartInfo.FileName = "bash"; // or "cmd" if running on Windows
        compilerProcess.StartInfo.Arguments = $"-c \"{command}\"";
        ;
        compilerProcess.StartInfo.RedirectStandardOutput = true;
        compilerProcess.StartInfo.RedirectStandardError = true;
        compilerProcess.StartInfo.UseShellExecute = false;
        compilerProcess.StartInfo.CreateNoWindow = true;

        Console.WriteLine($"rey {command}");
        compilerProcess.Start();
        Console.WriteLine("rey2");

        compilerProcess.WaitForExit(2000);
        Console.WriteLine("koniec czekanie");
        if (compilerProcess.HasExited)
        {
            // Check the exit code
            if (compilerProcess.ExitCode == 0)
            {
                Console.WriteLine("błądu niebył");
                return true;
            }

            Console.WriteLine("był błąd ");
            return false;
        }

        Console.WriteLine("nie zostało skończonne ");
        compilerProcess.Kill();
        return false;
    }
}