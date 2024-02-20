namespace Engine.BusinessLogic.Gameplay.Interface;

public interface ICorespondable
    {
        public Task<string?> Send(string data);
        public Task Interrupt();
        public void Run();
    }