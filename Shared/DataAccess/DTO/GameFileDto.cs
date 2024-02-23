using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.DAO;

public class GameFileDto
{
    public long PlayerId { get; set; }
    public long GameId { get; set; }   
    public string GameName { get; set; }
    public IFormFile file { get; set; }
}