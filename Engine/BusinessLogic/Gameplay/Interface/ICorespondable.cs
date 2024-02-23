namespace Engine.BusinessLogic.Gameplay.Interface;

public interface ICorespondable
    {
        public Task<bool> Send(string data);
        public Task<string?> Get();
        public Task Interrupt();
        public void Run();
    }