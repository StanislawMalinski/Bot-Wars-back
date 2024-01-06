using BusinessLogic.ExternalProgramServices.Corespondable;
using System.Diagnostics;

namespace BusinessLogic.ExternalProgramServices
{
    public class IOProgramWrapper : ICorespondable
    {
        private Process? _process;
        private bool isRunning;
        private string _path;

        // Program Configuration parameter should be added
        public IOProgramWrapper(string path) 
        { 
            _path = path;
        }

        // Not safe at all, maybe could be run as user without any privilages
        public void Run()
        {
            if (isRunning)
            {
                throw new Exception("Program already is running");
            }
            isRunning = true;
            _process = new Process();
            _process.StartInfo.FileName = _path;
            _process.Exited += new EventHandler(HandleExit);
            try
            {
                _process.Start();
            }
            catch (Exception ex)
            {
                throw new Exception($"Program cannot be started: {ex.Message}");
            }
            _process.BeginOutputReadLine();
        }

        public void Interrupt()
        {
            _process.Close();
        }

        public string Send(string data)
        {
            if (!isRunning)
            {
                throw new Exception("Program is not running");
            }
            StreamWriter sw = _process.StandardInput;
            sw.WriteLine(data);
            StreamReader sr = _process.StandardOutput;
            return sr.ReadToEnd();
        }

        private void HandleExit(object? sender, EventArgs e)
        {
            throw new Exception("Program exited");
        }
    }
}
