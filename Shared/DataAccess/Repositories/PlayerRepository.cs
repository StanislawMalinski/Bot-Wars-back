using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext _dataContext;
        private readonly IPlayerMapper _playerMapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextRepository _userContextRepository;
        private readonly IGameTypeMapper _gameTypeMapper;
        private readonly IBotMapper _botMapper;

        public PlayerRepository(DataContext dataContext,
            IPlayerMapper playerMapper,
            IPasswordHasher passwordHasher,
            IAuthorizationService authorizationService,
            IUserContextRepository userContextRepository,
            IGameTypeMapper gameTypeMapper, IBotMapper botMapper)
        {
            _userContextRepository = userContextRepository;
            _authorizationService = authorizationService;
            _passwordHasher = passwordHasher;
            _dataContext = dataContext;
            _playerMapper = playerMapper;
            _gameTypeMapper = gameTypeMapper;
            _botMapper = botMapper;
        }

        public async Task<HandlerResult<Success, IErrorResult>> CreateAdminAsync(
            RegistrationRequest registrationRequest)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
                2,
                new RoleNameToCreateAdminRequirement("Admin")).Result;
            if (!authorizationResult.Succeeded)
            {
                return new UnauthorizedError();
            }

            var emailPlayer = await _dataContext.Players
                .Where(player => player.Email == registrationRequest.Email)
                .FirstOrDefaultAsync();

            if (emailPlayer != null)
            {
                return new PlayerAlreadyExistsError
                {
                    Title = "PlayerAlreadyExistsError 400",
                    Message = "Player with given email already exists"
                };
            }

            var loginPlayer = await _dataContext.Players
                .Where(player => player.Login == registrationRequest.Login)
                .FirstOrDefaultAsync();

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
            await _dataContext.Players.AddAsync(newAdmin);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(
            RegistrationRequest registrationRequest)
        {
            var newPlayer = _playerMapper.ToPlayerFromRegistrationRequest(registrationRequest);

            if (newPlayer is null)
            {
                return new EntityNotFoundErrorResult();
            }

            var emailPlayer = await _dataContext.Players
                .Where(player => player.Email == registrationRequest.Email)
                .FirstOrDefaultAsync();

            if (emailPlayer != null)
            {
                return new PlayerAlreadyExistsError
                {
                    Title = "PlayerAlreadyExistsError 400",
                    Message = "Player with given email already exists"
                };
            }

            var loginPlayer = await _dataContext.Players
                .Where(player => player.Login == registrationRequest.Login)
                .FirstOrDefaultAsync();

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
            await _dataContext.Players.AddAsync(newPlayer);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest password,
            long? playerId)
        {
            if (playerId is null)
            {
                return new UnauthorizedError();
            }

            var player = await _dataContext.Players.FindAsync(playerId);
            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }

            var passwordMatchResult =
                await _passwordHasher.VerifyPasswordHash(password.Password, player.HashedPassword);
            if (!passwordMatchResult.IsError)
            {
                var hashedPassword = (await _passwordHasher.HashPassword(password.ChangePassword))
                    .Match(x => x.Data, x => null);
                player.HashedPassword = hashedPassword;
                await _dataContext.SaveChangesAsync();
                return new Success();
            }

            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Hasło nie poprawne"
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> ChangeLogin(ChangeLoginRequest changeLoginRequest,
            long? playerId)
        {
            if (playerId is null)
            {
                return new UnauthorizedError();
            }

            var player = await _dataContext
                .Players
                .FindAsync(playerId);

            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }

            if (player.Login != changeLoginRequest.Login)
                return new BadAccountInformationError
                {
                    Title = "BadAccountInformationError 400",
                    Message = "Given login does not match"
                };

            player.Login = changeLoginRequest.NewLogin;
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id)
        {
            var player = await _dataContext.Players.FindAsync(id);
            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }
            player.Email = "";
            player.Login = "deleted_" + player.Id.ToString();
            player.Deleted = true;
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id)
        {
            var resPlayer = await _dataContext.Players
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (resPlayer is null)
            {
                return new EntityNotFoundErrorResult();
            }

            return new SuccessData<PlayerDto>()
            {
                Data = _playerMapper.ToDto(resPlayer)
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> SetPlayerLastLogin(string email, DateTime lastLogin)
        {
            var player = await _dataContext.Players
                .FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }

            player.LastLogin = DateTime.Now;
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<PlayerInternalDto>, IErrorResult>> GetPlayerAsync(string email)
        {
            var resPlayer = await _dataContext.Players
                .Include(p => p.Role)
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (resPlayer is null)
            {
                return new EntityNotFoundErrorResult();
            }

            return new SuccessData<PlayerInternalDto>()
            {
                Data = _playerMapper.ToInternalDto(resPlayer)
            };
        }

        public async Task<HandlerResult<SuccessData<List<PlayerDto>>, IErrorResult>> GetPlayersAsync()
        {
            var resPlayers = await _dataContext.Players
                .ToListAsync();

            var playerDtos = resPlayers.Select(player => _playerMapper.ToDto(player)).ToList();

            return new SuccessData<List<PlayerDto>>()
            {
                Data = playerDtos
            };
        }
        
        public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId)
        {
            var player = await _dataContext
                .Players
                .FindAsync(playerId);

            if (player == null)
            {
                return new EntityNotFoundErrorResult()
                {
                    Message = "EntityNotFound 404",
                    Title = "Player with given id does not exist"
                };
            }

            var bots = await _dataContext
                .Bots
                .Where(bot => bot.PlayerId == playerId)
                .Select(bot => _botMapper.MapBotToResponse(bot))
                .ToListAsync();

            return new SuccessData<List<BotResponse>>
            {
                Data = bots
            };
        }

        public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long? playerId)
        {
            if (playerId is null)
            {
                return new EntityNotFoundErrorResult();
            }

            var player = await _dataContext.Players.FindAsync(playerId);

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
            playerInfo.BotsNumber = _dataContext.Bots.Count(x => x.PlayerId == playerId);
            playerInfo.TournamentNumber = _dataContext.Tournaments.Count(x => x.CreatorId == playerId);
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

            var player = await _dataContext.Players.FirstOrDefaultAsync(p => p.Login.Equals(playerName));

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
            playerInfo.BotsNumber = _dataContext.Bots.Count(x => x.PlayerId == player.Id);
            playerInfo.TournamentNumber = _dataContext.Tournaments.Count(x => x.CreatorId == player.Id);
            return new SuccessData<PlayerInfo>()
            {
                Data = playerInfo
            };
        }

        public async Task<HandlerResult<SuccessData<List<GameSimpleResponse>>, IErrorResult>> GetMyGames(long playerId)
        {
            
            var res = await _dataContext.Games.Where(x => x.CreatorId == playerId).Select(x=> _gameTypeMapper.MapGameToSimpleResponse(x)).ToListAsync();
            return new SuccessData<List<GameSimpleResponse>>()
            {
                Data = res
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> ChangeImage(PlayerImageRequest imageRequest, long playerId)
        {
            var res = await _dataContext.Players.FindAsync(playerId);
            if (res == null) return new EntityNotFoundErrorResult();
            res.Image = Convert.FromBase64String(imageRequest.Image!);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GetImage(long playerId)
        {
            var res = await _dataContext.Players.FindAsync(playerId);
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
}