using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class AchievementsRepository : IAchievementsRepository
{
    private readonly DataContext _dataContext;

    public AchievementsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<AchievementType?> GetAchievementTypeById(long achievementTypeId)
    {
        return await _dataContext
            .AchievementType
            .FindAsync(achievementTypeId);
    }
    
    public async Task<List<AchievementRecord>> GetAchievementRecordsByPlayerId(long playerId)
    {
        return await _dataContext
            .AchievementRecord
            .Include(x=>x.AchievementType)
            .Where(x => x.PlayerId == playerId)
            .ToListAsync();
    }

    public async Task<List<AchievementThresholds>> GetAchievementRecordByAchievementRecord(AchievementRecord achievementRecord)
    {
        return await _dataContext.AchievementThresholds
            .Where(x => x.Threshold <= achievementRecord.Value && x.AchievementTypeId == achievementRecord.AchievementTypeId)
            .ToListAsync();
    }

    public async Task<List<AchievementRecord>> GetAchievementRecordsByPlayerIdAndAchievementTypeId(long playerId, long achievementTypeId)
    {
        return await _dataContext
            .AchievementRecord
            .Where(record => record.PlayerId == playerId && record.AchievementTypeId == achievementTypeId)
            .ToListAsync();
    }

    public async Task<List<AchievementThresholds>> GetAchievementThresholdsByAchievementTypeId(long achievementTypeId)
    {
        return await _dataContext
            .AchievementThresholds
            .Where(threshold => threshold.AchievementTypeId == achievementTypeId)
            .ToListAsync();
    }

    public async Task<List<AchievementType>> GetAchievementTypes()
    {
        return await _dataContext
            .AchievementType
            .Include(type => type.AchievementThresholds)
            .ToListAsync();
    }

    public async void AddAchievementRecord(AchievementRecord obtainedAchievement)
    {
        await _dataContext
            .AchievementRecord
            .AddAsync(obtainedAchievement);
        SaveAsync();
    }

    private async void SaveAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
    
    public async Task<bool> UpDateProgressNoSave(AchievementsTypes type, long botId)
    {
        var botRes = await _dataContext.Bots.FindAsync(botId);
        if (botRes == null) return false;
        long userId = botRes.PlayerId;
        var res = await _dataContext.AchievementRecord.FirstOrDefaultAsync(x =>
            x.AchievementTypeId == (long)type && x.PlayerId == userId);
        long value;
        if (res == null)
        {
            await _dataContext.AchievementRecord.AddAsync(new AchievementRecord()
            {
                AchievementTypeId = (long) type,
                Value = 1,
                PlayerId = userId,
            });
            value = 1;
        }
        else
        {
            res.Value++;
            value = res.Value;
        }

        var notification = await _dataContext.AchievementThresholds.FirstOrDefaultAsync(x =>
            x.AchievementTypeId == (long)type && x.Threshold == value);
        if (notification != null)
        {
            await _dataContext.NotificationOutboxes.AddAsync(new NotificationOutbox()
            {
                Type = NotificationType.Achievement,
                NotificationValue = 1,
                PLayerId = userId
            });
        }
        
        return true;
    } 
    
}