using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DTO.Requests;

public class BotRequest
{
    [Required]
    public long GameId { get; set; }
    [Required]
    public IFormFile BotFile { get; set; }
    [Required]
    public Language Language { get; set; }
}