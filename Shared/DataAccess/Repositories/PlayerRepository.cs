using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.RepositoryInterfaces;

namespace Shared.DataAccess.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IBotMapper _botMapper;
    private readonly DataContext _dataContext;
    private readonly IGameTypeMapper _gameTypeMapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPlayerMapper _playerMapper;
    private readonly IUserContextRepository _userContextRepository;

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

    public async Task<Player?> GetPlayer(string playerEmail)
    {
        return await _dataContext.Players.Where(x => x.Email.Equals(playerEmail))
            .Include(x => x.Role).FirstOrDefaultAsync();
    }

    public async Task<List<Player>> GetPlayersByPartialName(string playerName, PageParameters pageParameters)
    {
        var players = await _dataContext.Players.Where(x => x.Login.Contains(playerName))
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .ToListAsync();
        return players;
    }

    public async Task<Player?> GetPlayerByLogin(string login)
    {
        return await _dataContext.Players.Where(x => x.Login.Equals(login))
            .Include(x => x.Role)
            .FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task<EntityEntry<Player>> AddPlayer(Player player)
    {
        var newRecord = new AchievementRecord
        {
            AchievementTypeId = (int)AchievementsTypes.AccountCreated,
            Player = player,
            Value = 1
        };
        player.AchievementRecords = new List<AchievementRecord>();
        player.AchievementRecords.Add(newRecord);
        return await _dataContext.Players.AddAsync(player);
    }


    public async Task<bool> DeletePlayerAsync(long id)
    {
        var player = await _dataContext.Players.FindAsync(id);
        if (player is null) return false;
        player.Email = "";
        player.Login = "deleted_" + player.Id;
        player.Deleted = true;
        return true;
    }


    public async Task<List<BotResponse>> GetPlayerBots(long playerId)
    {
        return await _dataContext
            .Bots
            .Where(bot => bot.PlayerId == playerId)
            .Select(bot => _botMapper.MapBotToResponse(bot))
            .ToListAsync();
    }

    public async Task<bool> SetPlayerLastLogin(string email, DateTime lastLogin)
    {
        var player = await _dataContext.Players
            .FirstOrDefaultAsync(u => u.Email.Equals(email));

        if (player is null) return false;

        player.LastLogin = DateTime.Now;
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<Player?> GetPlayer(long playerId)
    {
        return await _dataContext.Players.Where(x => x.Id == playerId)
            .Include(x => x.Role).FirstOrDefaultAsync();
    }


    public async Task<int> PlayerBotCount(long playerId)
    {
        return await _dataContext.Bots.CountAsync(x => x.PlayerId == playerId);
    }

    public async Task<int> PlayerTournamentCount(long playerId)
    {
        return await _dataContext.Tournaments.CountAsync(x => x.CreatorId == playerId);
    }


    public async Task<List<GameSimpleResponse>> GetMyGames(long playerId)
    {
        return await _dataContext.Games.Where(x => x.CreatorId == playerId)
            .Select(x => _gameTypeMapper.MapGameToSimpleResponse(x)).ToListAsync();
    }
}