using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public interface IPlayerMapper
{
    public PlayerDto? ToDto(Player? player);
    public Player? ToPlayerEntity(PlayerDto? playerDto);
    public PlayerResponse ToPlayerResponse(Player player);
}