using Shared.DataAccess.DAO;
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
            Id = player.Id,
            Email = player.Email,
            Login = player.Login,
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
            Id = playerDto.Id,
            Email = playerDto.Email,
            Login = playerDto.Login,
            //TODO hashowanie
            HashedPassword = playerDto.Password,
            isBanned = playerDto.isBanned,
            Points = playerDto.Points,
        };
    }
}