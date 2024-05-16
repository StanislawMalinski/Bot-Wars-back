using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BotWars;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.AuthorizationRequirements;
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
    
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextRepository _userContextRepository;
    private readonly IGameTypeMapper _gameTypeMapper;
    private readonly IBotMapper _botMapper;

    public PlayerService(IPlayerRepository playerRepository,
        AuthenticationSettings settings,
        IPlayerMapper playerMapper,
        IPasswordHasher passwordHasher,
        ITournamentService tournamentService,
        IAuthorizationService authorizationService,
        IUserContextRepository userContextRepository,
        IGameTypeMapper gameTypeMapper, IBotMapper botMapper)
    {
        _passwordHasher = passwordHasher;
        _settings = settings;
        _playerMapper = playerMapper;
        _playerRepository = playerRepository;
        _tournamentService = tournamentService;
        _gameTypeMapper = gameTypeMapper;
        _botMapper = botMapper;
        _userContextRepository = userContextRepository;
        _authorizationService = authorizationService;
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long playerId)
    {
        var resPlayer = await _playerRepository.GetPlayer(playerId);
        if (resPlayer is null)
        {
            return new EntityNotFoundErrorResult();
        }

        return new SuccessData<PlayerDto>()
        {
            Data = _playerMapper.ToDto(resPlayer)
        };
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewPlayer(RegistrationRequest registrationRequest)
    {
        var newPlayer = _playerMapper.ToPlayerFromRegistrationRequest(registrationRequest);

        if (newPlayer is null)
        {
            return new EntityNotFoundErrorResult();
        }

        var emailPlayer = await _playerRepository.GetPlayer(registrationRequest.Email);

        if (emailPlayer != null)
        {
            return new PlayerAlreadyExistsError
            {
                Title = "PlayerAlreadyExistsError 400",
                Message = "Player with given email already exists"
            };
        }

        var loginPlayer = await _playerRepository.GetPlayerByLogin(registrationRequest.Login);

        if (loginPlayer != null)
        {
            return new PlayerAlreadyExistsError
            {
                Title = "PlayerAlreadyExistsError 400",
                Message = "Player with given login already exists"
            };
        }

        newPlayer.RoleId = 1;
        var hashedPassword =
            (await _passwordHasher.HashPassword(registrationRequest.Password)).Match(x => x.Data, x => null);
        newPlayer.HashedPassword = hashedPassword;
        await _playerRepository.AddPlayer(newPlayer);
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> RegisterNewAdmin(RegistrationRequest registrationRequest)
    {
        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            2,
            new RoleNameToCreateAdminRequirement("Admin")).Result;
        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }


        var emailPlayer = await _playerRepository.GetPlayer(registrationRequest.Email);

        if (emailPlayer != null)
        {
            return new PlayerAlreadyExistsError
            {
                Title = "PlayerAlreadyExistsError 400",
                Message = "Player with given email already exists"
            };
        }

        var loginPlayer = await _playerRepository.GetPlayerByLogin(registrationRequest.Login);

        if (loginPlayer != null)
        {
            return new PlayerAlreadyExistsError
            {
                Title = "PlayerAlreadyExistsError 400",
                Message = "Player with given login already exists"
            };
        }

        var newAdmin = _playerMapper.ToPlayerFromRegistrationRequest(registrationRequest);

        if (newAdmin is null)
        {
            return new EntityNotFoundErrorResult();
        }

        newAdmin.RoleId = 2;
        var hashedPassword =
            (await _passwordHasher.HashPassword(registrationRequest.Password)).Match(x => x.Data, x => null);
        newAdmin.HashedPassword = hashedPassword;
        await _playerRepository.AddPlayer(newAdmin);
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id)
    {
        await _tournamentService.DeleteUserScheduledTournaments(id);
        if (!await _playerRepository.DeletePlayerAsync(id))
        {
            return new EntityNotFoundErrorResult();
        }
        await _playerRepository.SaveChangesAsync();
        return new Success();
       
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetPlayerBots(long id)
    {
        var player = await _playerRepository.GetPlayer(id);
        
        if (player == null) return new EntityNotFoundErrorResult();
        
        return new SuccessData<List<BotResponse>>
        {
            Data = await _playerRepository.GetPlayerBots(id)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest request, long playerId)
    {


        var player = await _playerRepository.GetPlayer(playerId);
        if (player is null)
        {
            return new EntityNotFoundErrorResult();
        }

        var passwordMatchResult =
            await _passwordHasher.VerifyPasswordHash(request.Password, player.HashedPassword);
        if (!passwordMatchResult.IsError)
        {
            var hashedPassword = (await _passwordHasher.HashPassword(request.ChangePassword))
                .Match(x => x.Data, x => null);
            player.HashedPassword = hashedPassword;
            await _playerRepository.SaveChangesAsync();
            return new Success();
        }

        return new BadAccountInformationError()
        {
            Title = "Return null",
            Message = "Hasło nie poprawne"
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest request, long playerId)
    {
        
        var player = await _playerRepository.GetPlayer(playerId);

        if (player is null)
        {
            return new EntityNotFoundErrorResult();
        }

        if (player.Login != request.Login)
            return new BadAccountInformationError
            {
                Title = "BadAccountInformationError 400",
                Message = "Given login does not match"
            };

        player.Login = request.NewLogin;
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto)
    {
        var player = await _playerRepository.GetPlayer(dto.Email);
        
        if (player is null)
        {
            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Niepoprawny email lub haslo"
            };
        }
        
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
            new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
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
        if (!saveLastResult)
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

    public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long playerId)
    {
        var player = await _playerRepository.GetPlayer(playerId);

        if (player is null)
        {
            return new EntityNotFoundErrorResult();
        }

        PlayerInfo playerInfo = new PlayerInfo
        {
            Login = player.Login,
            Registered = player.Registered,
            Point = player.Points,
            Id = player.Id
        };
        playerInfo.BotsNumber = await _playerRepository.PlayerBotCount(playerId);
        playerInfo.TournamentNumber = await _playerRepository.PlayerTournamentCount(playerId);
        return new SuccessData<PlayerInfo>()
        {
            Data = playerInfo
        };
    }

    public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(string? playerName)
    {
        if (playerName is null)
        {
            return new EntityNotFoundErrorResult();
        }

        var player = await _playerRepository.GetPlayerByLogin(playerName);

        if (player is null)
        {
            return new EntityNotFoundErrorResult();
        }

        PlayerInfo playerInfo = new PlayerInfo
        {
            Login = player.Login,
            Registered = player.Registered,
            Point = player.Points,
            Id = player.Id
        };
        playerInfo.BotsNumber = await _playerRepository.PlayerBotCount(player.Id);
        playerInfo.TournamentNumber = await _playerRepository.PlayerTournamentCount(player.Id);
        return new SuccessData<PlayerInfo>()
        {
            Data = playerInfo
        };
    }

    public async Task<HandlerResult<SuccessData<List<GameSimpleResponse>>, IErrorResult>> GetGamesForPlayer(long playerId)
    {
        return new SuccessData<List<GameSimpleResponse>>()
        {
            Data = await _playerRepository.GetMyGames(playerId)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ChangePlayerImage(PlayerImageRequest imageRequest, long playerId)
    {
        if (imageRequest.Image == null) return new IncorrectOperation();
        if (imageRequest.Image.Length % 4 != 0) return new IncorrectOperation(){Message = "to nie jest string base 64 musi miec wielkosc podzielna przez 4"};
        var res = await _playerRepository.GetPlayer(playerId);
        if (res == null) return new EntityNotFoundErrorResult();
        res.Image = Convert.FromBase64String(imageRequest.Image!);
        await _playerRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GetPlayerImage(long playerId)
    {
        var res = await _playerRepository.GetPlayer(playerId);
        if (res == null) return new EntityNotFoundErrorResult();
        if (res.Image == null)  return new SuccessData<string>
        {
            Data = string.Empty
        };
        return new SuccessData<string>
        {
            Data = Convert.ToBase64String(res.Image)
        };
    }
}