namespace Shared.DataAccess.DTO.Responses;

public class BotResponse
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public long GameId { get; set; }
    public long FileId { get; set; }
    
    public int MemoryUsed { get; set; }
    public int TimeUsed { get; set; }
    public string? Validation { get; set; }
    
}