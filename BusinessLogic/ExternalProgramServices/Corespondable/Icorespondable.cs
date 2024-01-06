namespace BusinessLogic.ExternalProgramServices.Corespondable
{
    public interface ICorespondable
    {
        public string Send(string data);
        public void Interrupt();
    }
}
