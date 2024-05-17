using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
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
            Deleted = player.Deleted,
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
            Role = player.Role,
            Deleted = player.Deleted
        };
    }

    public Player? ToPlayerFromRegistrationRequest(RegistrationRequest? registrationRequest)
    {
        if (registrationRequest == null)
        {
            return null;
        }

        return new Player
        {
            Email = registrationRequest.Email,
            Login = registrationRequest.Login,
            HashedPassword = registrationRequest.Password,
            Points = 1000,
            isBanned = false,
            Registered = DateTime.Now,
            Role = new Role()
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
            Deleted = playerDto.Deleted,
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
            Role = playerDto.Role,
            Deleted = playerDto.Deleted
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