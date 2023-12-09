namespace BusinessLogic.ExternalProgramServices.Corespondable
{
    public interface ICorespondable
    {
        public Task<string?> Send();
        public void Interrupt();
    }
}
