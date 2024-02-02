namespace Shared.DataAccess.DataBaseEntities;

public class UserSettings
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public Player Player { get; set; }
    public bool IsDarkTheme { get; set; }
    public string Language { get; set; }
}