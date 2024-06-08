namespace Engine.BusinessLogic.Gameplay;

public class FileDto
{
    public long PlayerId { get; set; }
    public long GameId { get; set; }
    public long BotId { get; set; }
    public string BotName { get; set; }
    public string FileContent { get; set; }
}