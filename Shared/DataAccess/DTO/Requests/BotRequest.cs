using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.DTO.Requests;

public class BotRequest
{
    [Required]
    public long GameId { get; set; }
    [Required]
    public IFormFile BotFile { get; set; }
}