using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IPlayerMapper
{
    public PlayerDto? ToDto(Player? player);
    public Player? ToPlayerEntity(PlayerDto? playerDto);
}