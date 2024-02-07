using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DataBaseEntities;

public class _Task
{
    public long Id { get; set; }
    public TaskTypes Type { get; set; }
    public DateTime ScheduledOn { get; set; }
    public string? Status { get; set; }
    public long Refid { get; set; }
    
    public _Task ParentTask { get; set; }
    public List<_Task> ChildrenTask { get; set; }
    public long ParentTaskId { get; set; }
    
}