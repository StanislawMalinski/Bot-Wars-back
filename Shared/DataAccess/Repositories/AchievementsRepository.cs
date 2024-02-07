using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
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
    private readonly IAchievementRecordMapper _recordMapper;
    private readonly IAchievementTypeMapper _typeMapper;

    public AchievementsRepository(DataContext dataContext, IAchievementRecordMapper recordMapper, IAchievementTypeMapper typeMapper)
    {
        _dataContext = dataContext;
        _recordMapper = recordMapper;
        _typeMapper = typeMapper;
    }
    
    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>>
        GetAchievementsForPlayer(long playerId)
    {
        var player = await _dataContext
            .Players
            .FindAsync(playerId);
        
        if (player == null)
        {
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Player could not have been found"
            };
        }
        
        var achievementRecords = await _dataContext.AchievementRecord.Include(x=>x.AchievementType).Where(x => x.PlayerId == playerId).ToListAsync();
        var result = new List<AchievementRecordDto>();
        foreach (var var in achievementRecords)
        {
            result.AddRange((await _dataContext.AchievementThresholds.Where(x=>x.Threshold<=var.Value&& x.AchievementTypeId == var.AchievementTypeId).ToListAsync()).ConvertAll(x=>_recordMapper.ToDto(var,x)));
        }

        return new SuccessData<List<AchievementRecordDto>>
        {
            Data = result
        };
    }
    //a ja wpadłem XD
    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(
        long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        var player = await _dataContext
            .Players
            .FindAsync(playerId);

        if (player == null)
        {
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Player could not have been found"
            };
        }

        var achievementType = await _dataContext
            .AchievementType
            .FindAsync(achievementTypeId);

        if (achievementType == null)
        {
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Achievement type could not have been found"
            };
        }

        var achievementRecords = await _dataContext
            .AchievementRecord
            .Where(record => record.PlayerId == playerId && record.AchievementTypeId == achievementTypeId)
            .ToListAsync();

        var achievementThresholds = await _dataContext
            .AchievementThresholds
            .Where(threshold => threshold.AchievementTypeId == achievementTypeId)
            .ToListAsync();

        if (achievementThresholds.Count <= achievementRecords.Count)
            return new AchievementsAlreadyObtainedError
            {
                Title = "AchievementsAlreadyObtainedError 400",
                Message = "You have already obtained all achievements from this type"
            };
        var nextThreshold = achievementRecords.Count;
        var currentThreshold = achievementThresholds.ElementAt(nextThreshold);
        if (currentThreshold.Threshold > currentPlayerThreshold)
        {
            return new NotEnoughAchievementPointsError
            {
                Title = "NotEnoughAchievementPointsError 400",
                Message = "You are not eligible for this achievement yet"
            };
        }

        var obtainedAchievement = new AchievementRecord
        {
            Value = currentThreshold.Threshold,
            AchievementTypeId = achievementType.Id,
            PlayerId = playerId
        };

        await _dataContext
            .AchievementRecord
            .AddAsync(obtainedAchievement);
        await _dataContext.SaveChangesAsync();
        
        return new Success();

    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        var achievementTypes = await _dataContext
            .AchievementType
            .Include(type => type.AchievementThresholds)
            .Select(type => _typeMapper.ToDto(type))
            .ToListAsync();

        return new SuccessData<List<AchievementTypeDto>>
        {
            Data = achievementTypes
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpDateProgress(AchievementsTypes type, long userId)
    {
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

        await _dataContext.SaveChangesAsync();
        return new Success();
    } 
    
}