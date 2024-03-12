using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BotWars;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.DataBaseEntities;
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
        private readonly IPasswordHasher<Player?> _passwordHasher;
        private readonly AuthenticationSettings _settings;

        public PlayerRepository(DataContext context, IPlayerMapper playerMapper, IPasswordHasher<Player> passwordHasher, AuthenticationSettings settings)
        {
            _settings = settings;
            _passwordHasher = passwordHasher;
            _context = context;
            _playerMapper = playerMapper;
        }

        public async Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto)
        {
            var player = await _context.Players
                .Include(p => p.Role)
                .FirstOrDefaultAsync(u => u.Email.Equals(dto.Email));

            if (player is null)
            {
                return new BadAccountInformationError()
                {
                    Title = "BadAccountInformationError 404",
                    Message = "Player could not have been found"
                };
            }

            var result = _passwordHasher.VerifyHashedPassword(player, player.HashedPassword, dto.Password);

            if (result == PasswordVerificationResult.Failed)
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
            return new SuccessData<string>()
            {
                Data = completeToken
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(PlayerDto playerDto)
        {
            var newPlayer = _playerMapper.ToPlayerEntity(playerDto);
            newPlayer.Points = 2000;
            var hashedPassword = _passwordHasher.HashPassword(newPlayer, newPlayer?.HashedPassword);
            newPlayer.HashedPassword = hashedPassword;
            var resPlayer = await _context.Players.AddAsync(newPlayer);
            await _context.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id)
        {
            var resPlayer = await _context.Players.FindAsync(id);
            if (resPlayer == null)
            {
                return new EntityNotFoundErrorResult()
                {
                    Title = "Return null",
                    Message = "Nie znaleziono gracza w bazie danych"
                };
            }

            _context.Remove(resPlayer);
            await _context.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id)
        {
            var resPlayer = await _context.Players.FindAsync(id);
            if (resPlayer == null)
            {
                return new EntityNotFoundErrorResult()
                {
                    Title = "return null",
                    Message = "Nie znaleziono gracza w bazie danych"
                };
            }

            return new SuccessData<PlayerDto>()
            {
                Data = _playerMapper.ToDto(resPlayer)
            };
        }

        public async Task<HandlerResult<SuccessData<List<PlayerDto>>, IErrorResult>> GetPlayersAsync()
        {
            var resPlayer = _context.Players.Select(x => _playerMapper.ToDto(x)).ToList();

            return new SuccessData<List<PlayerDto>>()
            {
                Data = resPlayer
            };
        }

        
    }
}