using Communication.ServiceInterfaces;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Achievement;

public class AchievementService : IAchievementService
{
    private readonly IAchievementsRepository _achievementsRepository;
    private readonly DataContext _dataContext;
    private readonly IPlayerRepository _playerRepository;
    private readonly IAchievementRecordMapper _recordMapper;
    private readonly IAchievementTypeMapper _typeMapper;

    public AchievementService(IAchievementsRepository achievementsRepository, DataContext dataContext,
        IPlayerRepository playerRepository, IAchievementRecordMapper recordMapper, IAchievementTypeMapper typeMapper)
    {
        _achievementsRepository = achievementsRepository;
        _dataContext = dataContext;
        _playerRepository = playerRepository;
        _recordMapper = recordMapper;
        _typeMapper = typeMapper;
    }

    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>>
        GetAchievementsForPlayer(long playerId)
    {
        var player = await _playerRepository
            .GetPlayer(playerId);

        if (player == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Player could not have been found"
            };

        var achievementRecords = await _achievementsRepository.GetAchievementRecordsByPlayerId(playerId);

        var result = new List<AchievementRecordDto>();
        var finalResult = new List<AchievementRecordDto>();
        foreach (var var in achievementRecords)
            result.AddRange((await _achievementsRepository.GetAchievementRecordByAchievementRecord(var))
                .ConvertAll(x => _recordMapper.ToDto(var, x)));

        var grouped = result.GroupBy(x => x.AchievementTypeId)
            .Select(g => g.ToArray())
            .ToArray();

        foreach (var v in grouped)
        {
            var max = v.Max(x => x.Value);
            v[0].Value = max;
            finalResult.Add(v[0]);
        }

        return new SuccessData<List<AchievementRecordDto>>
        {
            Data = finalResult
        };
    }


    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(
        long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        var player = await _playerRepository
            .GetPlayer(playerId);

        if (player == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Player could not have been found"
            };

        var achievementType = await _achievementsRepository.GetAchievementTypeById(achievementTypeId);

        if (achievementType == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundError 404",
                Message = "Achievement type could not have been found"
            };

        var achievementRecords =
            await _achievementsRepository.GetAchievementRecordsByPlayerIdAndAchievementTypeId(playerId,
                achievementTypeId);

        var achievementThresholds =
            await _achievementsRepository.GetAchievementThresholdsByAchievementTypeId(achievementTypeId);

        if (achievementThresholds.Count <= achievementRecords.Count)
            return new AchievementsAlreadyObtainedError
            {
                Title = "AchievementsAlreadyObtainedError 400",
                Message = "You have already obtained all achievements from this type"
            };
        var nextThreshold = achievementRecords.Count;
        var currentThreshold = achievementThresholds.ElementAt(nextThreshold);
        if (currentThreshold.Threshold > currentPlayerThreshold)
            return new NotEnoughAchievementPointsError
            {
                Title = "NotEnoughAchievementPointsError 400",
                Message = "You are not eligible for this achievement yet"
            };

        var obtainedAchievement = new AchievementRecord
        {
            Value = currentThreshold.Threshold,
            AchievementTypeId = achievementType.Id,
            PlayerId = playerId
        };

        _achievementsRepository.AddAchievementRecord(obtainedAchievement);

        return new Success();
    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        var achievementTypes = await _achievementsRepository.GetAchievementTypes();

        var results =
            achievementTypes.Select(type => _typeMapper.ToDto(type))
                .ToList();

        return new SuccessData<List<AchievementTypeDto>>
        {
            Data = results
        };
    }

    public Task GetAchievementIcon()
    {
        throw new NotImplementedException();
    }
}