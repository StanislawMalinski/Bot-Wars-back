using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.MappersInterfaces;

public interface IPlayerMapper
{
    public PlayerDto? ToDto(Player? player);
    public Player? ToPlayerEntity(PlayerDto? playerDto);
    public PlayerResponse ToPlayerResponse(Player player);
    public Player? ToPlayerInternalEntity(PlayerInternalDto? playerDto);
    public PlayerInternalDto? ToInternalDto(Player? player);
}