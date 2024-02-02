using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
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

        var achievementRecords = await _dataContext
            .AchievementRecord
            .Include(record => record.AchievementType)
            .Where(record => record.PlayerId == playerId)
            .ToListAsync();

        return new SuccessData<List<AchievementRecordDto>>
        {
            Data = achievementRecords.ConvertAll(record => _recordMapper.ToDto(record))
        };
    }
    //Nie jestem z tego za bardzo dumny, ale nie wpadłem na lepszy pomysł jak to zaimplementować z naszą architekturą osiągnięć XD
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
    
}