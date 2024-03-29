﻿using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class MatchResolver : Resolver
{
    private readonly TaskRepository _taskRepository;
    private readonly MatchRepository _matchRepository;
    private readonly AchievementHandlerService _achievementHandlerService;

    public MatchResolver(TaskRepository taskRepository, MatchRepository matchRepository, AchievementHandlerService achievementHandlerService)
    {
        _taskRepository = taskRepository;
        _matchRepository = matchRepository;
        _achievementHandlerService = achievementHandlerService;
    }

    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await _taskRepository.GetTask(taskId);
    }
    public async Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetBotsInMatch(long matchId)
    {
        return await _matchRepository.AllBots(matchId);
    }

    public async  Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetMatchGame(long matchId)
    {
        return await _matchRepository.GetGame(matchId);
    }

    public async  Task<HandlerResult<Success,IErrorResult>> MatchWinner(long matchId, long winner, long taskId)
    {
        return await _achievementHandlerService.MatchWinner(matchId, winner, taskId);
    }
}