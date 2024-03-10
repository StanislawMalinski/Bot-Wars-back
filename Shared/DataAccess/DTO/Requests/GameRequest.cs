using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.DTO.Requests;

public class GameRequest
{
    public int NumberOfPlayer { get; set; }
    [NotNull]
    public IFormFile? GameFile { get; set; }
    [NotNull]
    public string? GameInstructions { get; set; }
    [NotNull]
    public string? InterfaceDefinition { get; set; }
    public bool IsAvailableForPlay { get; set; }
}