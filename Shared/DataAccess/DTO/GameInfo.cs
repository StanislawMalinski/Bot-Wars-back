using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.DTO;

public struct GameInfo(bool played, int key, Bot? bot, List<Bot>? bots)
{
    public int Key = key;
    public bool ReadyToPlay = false;
    public bool Played { get; set; } = played;
    public Bot Bot = bot;
    public List<Bot>? Bots = bots;
}