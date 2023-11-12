namespace BotWars.GameTypeData
{
    public interface IGameTypeMapper
    {
        public GameTypeDto toDto(GameType gameType);
    }
}
