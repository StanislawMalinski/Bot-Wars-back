using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.MappersInterfaces;

public interface IPlayerMapper
{
    PlayerDto? ToDto(Player? player);
    Player? ToPlayerEntity(PlayerDto? playerDto);
    PlayerResponse ToPlayerResponse(Player player);
    Player? ToPlayerInternalEntity(PlayerInternalDto? playerDto);
    PlayerInternalDto? ToInternalDto(Player? player);
    Player? ToPlayerFromRegistrationRequest(RegistrationRequest registrationRequest);

}