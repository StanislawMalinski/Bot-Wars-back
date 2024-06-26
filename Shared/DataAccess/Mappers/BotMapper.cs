﻿using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.Mappers;

public class BotMapper : IBotMapper
{
    public Bot MapRequestToBot(BotRequest botRequest)
    {
        return new Bot
        {
            GameId = botRequest.GameId,
            BotFile = botRequest.BotFile.FileName,
            Language = botRequest.Language,
            Validation = BotStatus.ToScheduleForValidation
        };
    }

    public BotResponse MapBotToResponse(Bot bot)
    {
        return new BotResponse
        {
            Id = bot.Id,
            PlayerId = bot.PlayerId,
            GameId = bot.GameId,
            FileId = bot.FileId,
            Validation = bot.Validation.ToString(),
            MemoryUsed = bot.MemoryUsed,
            TimeUsed = bot.TimeUsed
        };
    }
}