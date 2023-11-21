using BotWars.Models;
using System.Diagnostics;

namespace BotWars.Services
{
    public class ProgramInstance
    {
        public readonly string _path;
        private Process? _process;
        private bool isRunning;
        private StreamWriter? _input;
        private readonly BusinessLogic.ExternalProgramServices.IGameManager _gameManager;
        private int _id;

        public ProgramInstance(IFileData fileData, BusinessLogic.ExternalProgramServices.IGameManager gameManager)
        {
            if (fileData.Data is null)
            {
                throw new ArgumentException("FileData Data cannot be null");
            }
            _path = Path.GetTempPath() + Guid.NewGuid().ToString() + fileData.Filename;
            File.WriteAllBytes(_path, fileData.Data);
            _gameManager = gameManager;
        }

        public ProgramInstance(string path, BusinessLogic.ExternalProgramServices.IGameManager gameManager)
        {
            _path = path;
            _gameManager = gameManager;
        }

        public void Run(int id)
        {   
            if (isRunning) 
            {
                throw new Exception("Program already is running");
            }
            isRunning = true;
            _process = new Process();
            _process.StartInfo.FileName = _path;
            _process.Exited += new EventHandler(HandleExit);
            _process.Start();
            _input = _process.StandardInput;
            _process.OutputDataReceived += HandleOutput;
            _process.BeginOutputReadLine();
        }

        public void SendData(string data)
        {
            if (!isRunning)
            {
                throw new Exception("Program is not running");
            }
            _input.WriteLine(data);
        }

        private void HandleOutput(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            _gameManager.HandleOutput(_id, data);
        }

        private void HandleExit(object sender, EventArgs e)
        {
            _gameManager.HandleExit(_id);
        }
    }
}
