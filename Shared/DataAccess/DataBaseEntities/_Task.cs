using Shared.DataAccess.Enumerations;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.DataBaseEntities;

public class _Task
{
    public long Id { get; set; }
    public TaskTypes Type { get; set; }
    public DateTime ScheduledOn { get; set; }
    public TaskStatus Status { get; set; }
    public long OperatingOn { get; set; }

    
}