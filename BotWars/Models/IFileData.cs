namespace BotWars.Models
{
    public interface IFileData
    {
        public String? Filename { get; set; }
        public byte[]? Data { get; set; }
    }
}
