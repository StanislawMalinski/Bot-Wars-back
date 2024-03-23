using Microsoft.AspNetCore.Authorization;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.DTO.Requests;
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
        private readonly DataContext _context;
        private readonly IPlayerMapper _playerMapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextRepository _userContextRepository;


        public PlayerRepository(DataContext context,
            IPlayerMapper playerMapper,
            IPasswordHasher passwordHasher,
            IAuthorizationService authorizationService,
            IUserContextRepository userContextRepository)
        {
            _userContextRepository = userContextRepository;
            _authorizationService = authorizationService;
            _passwordHasher = passwordHasher;
            _context = context;
            _playerMapper = playerMapper;
        }

        public async Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(PlayerDto playerDto)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
                playerDto.RoleId,
                new RoleNameToCreateAdminRequirement("Admin")).Result;
            if (!authorizationResult.Succeeded)
            {
                return new UnauthorizedError();
            }
            
            var newPlayer = _playerMapper.ToPlayerEntity(playerDto);
            newPlayer.Registered = DateTime.Now;
            newPlayer.Points = 2000;
            var hashedPassword = (await _passwordHasher.HashPassword(playerDto.Password)).Match(x => x.Data, x => null);
            newPlayer.HashedPassword = hashedPassword;
            var resPlayer = await _context.Players.AddAsync(newPlayer);
            await _context.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest password,
            long? playerId)
        {
            if (playerId is null)
            {
                return new UnauthorizedError();
            }

            var player = await _context.Players.FindAsync(playerId);
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
                await _context.SaveChangesAsync();
                return new Success();
            }

            return new BadAccountInformationError()
            {
                Title = "Return null",
                Message = "Hasło nie poprawne"
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }
            player.Email = "";
            player.Login = "deleted_" + player.Id.ToString();
            player.Deleted = true;
            await _context.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id)
        {
            var resPlayer = await _context.Players
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
            var player = await _context.Players
                .FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }

            player.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<PlayerInternalDto>, IErrorResult>> GetPlayerAsync(string email)
        {
            var resPlayer = await _context.Players
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
            var resPlayers = await _context.Players
                .ToListAsync();

            var playerDtos = resPlayers.Select(player => _playerMapper.ToDto(player)).ToList();

            return new SuccessData<List<PlayerDto>>()
            {
                Data = playerDtos
            };
        }

        public async Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long? playerId)
        {
            if (playerId is null)
            {
                return new EntityNotFoundErrorResult();
            }

            var player = await _context.Players.FindAsync(playerId);

            if (player is null)
            {
                return new EntityNotFoundErrorResult();
            }

            PlayerInfo playerInfo = new PlayerInfo
            {
                Login = player.Login,
                Registered = player.Registered,
                Point = player.Points
            };
            playerInfo.BotsNumber = _context.Bots.Count(x => x.PlayerId == playerId);
            playerInfo.TournamentNumber = _context.Tournaments.Count(x => x.CreatorId == playerId);
            return new SuccessData<PlayerInfo>()
            {
                Data = playerInfo
            };
        }
    }
}