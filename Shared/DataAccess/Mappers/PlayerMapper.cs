using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class PlayerMapper : IPlayerMapper
{
    public PlayerDto? ToDto(Player? player)
    {
        if (player == null)
        {
            return null;
        }

        return new PlayerDto
        {
            Email = player.Email,
            Login = player.Login,
            RoleId = player.RoleId,
            Password = player.HashedPassword,
            Points = player.Points,
            isBanned = player.isBanned
        };
    }

    public Player? ToPlayerEntity(PlayerDto? playerDto)
    {
        if (playerDto == null)
        {
            return null;
        }

        
        return new Player
        {
            Email = playerDto.Email,
            Login = playerDto.Login,
            RoleId = playerDto.RoleId,
            HashedPassword = playerDto.Password,
            isBanned = playerDto.isBanned,
            Points = playerDto.Points,
        };
    }
}