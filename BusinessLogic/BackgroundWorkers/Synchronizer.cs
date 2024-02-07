using Coravel.Invocable;

namespace BusinessLogic.BackgroundWorkers;

public class Synchronizer : IInvocable
{
    public Synchronizer()
    {
    }

    public async Task Invoke()
    {
        throw new NotImplementedException();
    }
}