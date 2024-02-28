using System;
using System.Threading;
using Engine.BusinessLogic.BackgroundWorkers;

namespace BusinessLogic.BackgroundWorkers
{
    public class ResourceManager
    {
        public readonly int MaxThreads;
        public ResourceManager(int maxThreads)
        {
            MaxThreads = maxThreads;
        }

        public async Task<List<TaskPerformer>> GetThreads()
        {
            return new List<TaskPerformer>();
        }
    }
}
