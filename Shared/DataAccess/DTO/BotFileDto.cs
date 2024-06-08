using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.DAO;

public class BotFileDto
{
    public long PlayerId { get; set; }
    public long GameId { get; set; }
    public long BotId { get; set; }
    public string BotName { get; set; }
    public IFormFile file { get; set; }
}