using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BotWars;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IPlayerMapper _playerMapper;
    private readonly AuthenticationSettings _settings;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITournamentService _tournamentService;

    public PlayerService(IPlayerRepository playerRepository,
        AuthenticationSettings settings,
        IPlayerMapper playerMapper,
        IPasswordHasher passwordHasher,
        ITournamentService tournamentService)
    {
        _passwordHasher = passwordHasher;
        _settings = settings;
        _playerMapper = playerMapper;
        _playerRepository = playerRepository;
        _tournamentService = tournamentService;
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        return await _playerRepository.GetPlayerAsync(PlayerId);
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(RegistrationRequest registrationRequest)
    {
        return await _playerRepository.CreatePlayerAsync(registrationRequest);
    }

    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewAdmin(RegistrationRequest registrationRequest)
    {
        return await _playerRepository.CreateAdminAsync(registrationRequest);
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id)
    {
        await _tournamentService.DeleteUserScheduledTournaments(id);
        return await _playerRepository.DeletePlayerAsync(id);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetPlayerBots(long id)
    {
        return await _playerRepository.GetBotsForPlayer(id);
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest request, long ?playerId)
    {
        return await _playerRepository.ChangePassword(request, playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest request, long? playerId)
    {
        return await _playerRepository.ChangeLogin(request, playerId);
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto)
    {
        var playerResult = await _playerRepository.GetPlayerAsync(dto.Email);
        if (playerResult.IsError)
        {
            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Niepoprawny email lub haslo"
            };
        }
        
        var playerResponse = playerResult.Match(x=>x.Data,x => null);
        
        if (playerResponse is null)
        {
            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Niepoprawny email lub haslo"
            };
        }
        
        var player = _playerMapper.ToPlayerInternalEntity(playerResponse);
        
        var passwordMatchResult = await _passwordHasher.VerifyPasswordHash(dto.Password, player.HashedPassword);

        if (passwordMatchResult.IsError)
        {
            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Niepoprawny email lub haslo"
            };
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, playerResponse.Id.ToString()),
            new Claim(ClaimTypes.Email, $"{player.Email}"),
            new Claim(ClaimTypes.Role, $"{player.Role.Name}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_settings.JwtExpireDays);

        var token = new JwtSecurityToken(_settings.JwtIssuer,
            _settings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        var completeToken = tokenHandler.WriteToken(token);
        var saveLastResult = await _playerRepository.SetPlayerLastLogin(dto.Email, DateTime.Now);
        if (saveLastResult.IsError)
        {
            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Coś poszło nie tak"
            };
        }
        return new SuccessData<string>()
        {
            Data = completeToken
        };
    }

    public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long ?playerId)
    {
        return await _playerRepository.GetPlayerInfoAsync(playerId);
    }

    public async Task<HandlerResult<SuccessData<List<GameSimpleResponse>>, IErrorResult>> GetMyGames(long playerId)
    {
        return await _playerRepository.GetMyGames(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangePlayerImage(PlayerImageRequest imageRequest, long playerId)
    {
        if (imageRequest.Image == null) return new IncorrectOperation();
        if (imageRequest.Image.Length % 4 != 0) return new IncorrectOperation(){Message = "to nie jest string base 64 musi miec wielkosc podzielna przez 4"};
        return await _playerRepository.ChangeImage(imageRequest, playerId);
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GetPlayerImage(long playerId)
    {
        return await _playerRepository.GetImage(playerId);
    }
}