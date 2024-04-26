using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.Enumerations;

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
    [NotNull]
    public Language Language { get; set; }
    public bool IsAvailableForPlay { get; set; }
}