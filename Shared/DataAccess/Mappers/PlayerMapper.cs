using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.MappersInterfaces;

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
            isBanned = player.isBanned,
        };
    }
    
    public PlayerInternalDto? ToInternalDto(Player? player)
    {
        if (player == null)
        {
            return null;
        }

        return new PlayerInternalDto
        {
            Id = player.Id,
            Email = player.Email,
            Login = player.Login,
            RoleId = player.RoleId,
            Password = player.HashedPassword,
            Points = player.Points,
            isBanned = player.isBanned,
            Role = player.Role
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
            Points = playerDto.Points
        };
    }
    
    public Player? ToPlayerInternalEntity(PlayerInternalDto? playerDto)
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
            Role = playerDto.Role
        };
    }

    public PlayerResponse ToPlayerResponse(Player player)
    {
        return new PlayerResponse()
        {
            Id = player.Id,
            Login = player.Login,
            Points = player.Points
        };
    }
}