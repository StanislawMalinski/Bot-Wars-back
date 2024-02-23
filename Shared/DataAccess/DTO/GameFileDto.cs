using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.DTO;

public class GameFileDto
{
    public long PlayerId { get; set; }
    public long GameId { get; set; }   
    public string GameName { get; set; }
    public IFormFile file { get; set; }
}