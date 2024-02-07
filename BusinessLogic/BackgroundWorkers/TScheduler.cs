using Coravel.Invocable;
using Shared.DataAccess.Context;

namespace BusinessLogic.BackgroundWorkers;

public class TScheduler : IInvocable
{
    private TaskDataContext _taskDataContext;
    public TScheduler()
    {
    }

    public async Task Invoke()
    {
        Console.WriteLine("hiii");
        await Task.Delay(5000);
        Console.WriteLine("koniechi");
    }
}