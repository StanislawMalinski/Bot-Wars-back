using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories
{
    public class PlayerRepository
    {
        private readonly DataContext _context;
        private readonly IPlayerMapper _playerMapper;

        public PlayerRepository(DataContext context, IPlayerMapper playerMapper)
        {
            _context = context;
            _playerMapper = playerMapper;
        }

        public async Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(PlayerDto playerDto)
        {
            var resPlayer = await _context.Players.AddAsync(_playerMapper.ToPlayerEntity(playerDto));
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

        public async Task<HandlerResult<Success, IErrorResult>> UpdatePlayerAsync(PlayerDto playerDto)
        {
            var resPlayer = await _context.Players.FindAsync(playerDto.Id);
            if (resPlayer == null)
            {
                return new EntityNotFoundErrorResult()
                {
                    Title = "Return null",
                    Message = "Nie ma gracza w bazie danych"
                };
            }

            var player = _playerMapper.ToPlayerEntity(playerDto);
            _context.Update(player);
            await _context.SaveChangesAsync();
            return new Success();
        }
    }
}