﻿using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament;

public class TournamentService : ITournamentService
{
    private readonly IBotRepository _botRepository;
    private readonly IGameRepository _gameRepository;
    private readonly ITournamentMapper _mapper;
    private readonly IPlayerRepository _playerRepository;
    private readonly TournamentRepository _tournamentRepository;

    public TournamentService(TournamentRepository tournamentRepository, IGameRepository gameRepository,
        ITournamentMapper tournamentMapper, IPlayerRepository playerRepository, IBotRepository botRepository)
    {
        _tournamentRepository = tournamentRepository;
        _gameRepository = gameRepository;
        _mapper = tournamentMapper;
        _playerRepository = playerRepository;
        _botRepository = botRepository;
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddTournament(long userId,
        TournamentRequest tournamentRequest)
    {
        if (tournamentRequest.Image != null && tournamentRequest.Image.Length % 4 != 0)
            return new IncorrectOperation
                { Message = "to nie jest string base 64 musi miec wielkosc podzielna przez 4" };

        var game = await _gameRepository.GetGame(tournamentRequest.GameId);
        if (game is null) return new EntityNotFoundErrorResult();


        var tournament = _mapper
            .TournamentRequestToTournament(tournamentRequest);
        tournament.CreatorId = userId;
        await _tournamentRepository.AddTournament(tournament);
        await _tournamentRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id, long playerId)
    {
        var authorizationResult = await _playerRepository.GetPlayer(playerId);

        if (authorizationResult == null || (!await _tournamentRepository.TournamentCreator(id, playerId) &&
                                            authorizationResult.RoleId != 2))
            return new NotTournamentCreatorError
            {
                Title = "NotTournamentCreatorError 400",
                Message = "You cannot delete tournament, that you did not create while you are not an admin"
            };

        var tournament = await _tournamentRepository.GetTournament(id);

        if (tournament is not { Status: TournamentStatus.SCHEDULED or TournamentStatus.NOTSCHEDULED })
            return new TournamentIsBeingPlayedError
            {
                Title = "Tournament cannot be deleted 400",
                Message = "Tournament cannot be deleted"
            };
        if (tournament.TournamentsDate < DateTime.Now.AddMinutes(10))
            return new TournamentIsBeingPlayedError
            {
                Title = "Tournament cannot be deleted 400",
                Message = "Tournament is to soon to star"
            };

        await _tournamentRepository.TournamentTaskDelete(id);
        await _tournamentRepository.DeleteTournamentAsync(id);
        await _tournamentRepository.SaveChangesAsync();


        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteUserScheduledTournaments(long userId)
    {
        var tournaments = await _tournamentRepository.GetPlayerScheduledTournaments(userId);
        foreach (var tournament in tournaments)
        {
            var tasks = await _tournamentRepository.GetScheduledTournamentTask(tournament.Id);
            foreach (var task in tasks) await _tournamentRepository.DeleteTask(task);

            await _tournamentRepository.DeleteTournamentAsync(tournament.Id);
        }

        await _tournamentRepository.SaveChangesAsync();
        return new Success();
    }


    public async Task<HandlerResult<SuccessData<PageResponse<TournamentResponse>>, IErrorResult>>
        GetListOfTournamentsFiltered(
            TournamentFilterRequest tournamentFilterRequest, PageParameters pageParameters)
    {
        var unfilteredTournaments = _tournamentRepository.GetTournamentsQuery();


        if (tournamentFilterRequest.MaxPlayOutDate != null)
            unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                tournament.TournamentsDate <= tournamentFilterRequest.MaxPlayOutDate);

        if (tournamentFilterRequest.MinPlayOutDate != null)
            unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                tournament.TournamentsDate >= tournamentFilterRequest.MinPlayOutDate);

        if (tournamentFilterRequest.Creator != null && !tournamentFilterRequest.Creator.Equals(string.Empty))
            unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                tournamentFilterRequest.Creator == null
                || tournamentFilterRequest.Creator.Equals(tournament.Creator.Login));

        if (tournamentFilterRequest.TournamentTitle != null &&
            !tournamentFilterRequest.TournamentTitle.Equals(string.Empty))
            unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                tournament.TournamentTitle.Contains(tournamentFilterRequest.TournamentTitle));


        var count = unfilteredTournaments.Count();


        if (tournamentFilterRequest.UserParticipation == null ||
            tournamentFilterRequest.UserParticipation.Equals(string.Empty))
        {
            if (Math.Min(pageParameters.PageSize,
                    Math.Max(0, count - pageParameters.PageNumber * pageParameters.PageSize)) == 0)
                return new SuccessData<PageResponse<TournamentResponse>>
                {
                    Data = new PageResponse<TournamentResponse>(new List<TournamentResponse>(),
                        pageParameters.PageSize, count)
                };
            var tournaments = await unfilteredTournaments
                .Select(tournament => _mapper.TournamentToTournamentResponse(tournament))
                .Skip(pageParameters.PageNumber * pageParameters.PageSize)
                .Take(Math.Min(pageParameters.PageSize,
                    Math.Max(0, count - pageParameters.PageNumber * pageParameters.PageSize)))
                .ToListAsync();
            return new SuccessData<PageResponse<TournamentResponse>>
            {
                Data = new PageResponse<TournamentResponse>(tournaments, pageParameters.PageSize, count)
            };
        }


        var filteredTournaments = await unfilteredTournaments
            .Select(tournament => _mapper.TournamentToTournamentResponse(tournament))
            .ToListAsync();

        var filteredTournamentList = new List<TournamentResponse>();

        foreach (var tournamentResponse in filteredTournaments)
            if ((await PlayerParticipate(tournamentResponse.Id, tournamentFilterRequest.UserParticipation))
                .IsSuccess)
                filteredTournamentList.Add(tournamentResponse);

        count = filteredTournamentList.Count();

        if (Math.Min(pageParameters.PageSize,
                Math.Max(0, count - pageParameters.PageNumber * pageParameters.PageSize)) == 0)
            return new SuccessData<PageResponse<TournamentResponse>>
            {
                Data = new PageResponse<TournamentResponse>(new List<TournamentResponse>(), pageParameters.PageSize,
                    count)
            };

        var pagedFilteredTournamentList = filteredTournamentList
            .Skip(Math.Min(pageParameters.PageNumber * pageParameters.PageSize, count))
            .Take(Math.Min(pageParameters.PageSize,
                Math.Max(0, count - pageParameters.PageNumber * pageParameters.PageSize)))
            .ToList();

        return new SuccessData<PageResponse<TournamentResponse>>
        {
            Data = new PageResponse<TournamentResponse>(pagedFilteredTournamentList, pageParameters.PageSize, count)
        };
    }

    public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id)
    {
        var tournament = await _tournamentRepository.GetTournamentExtended(id);

        if (tournament == null) return new EntityNotFoundErrorResult();
        return new SuccessData<TournamentResponse>
        {
            Data = _mapper.TournamentToTournamentResponse(tournament)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId,
        long playerId)
    {
        var tournament = await _tournamentRepository.GetTournament(tournamentId);

        if (tournament == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No tournament with such id has been found"
            };

        var bot = await _botRepository.GetBotAndCreator(botId);

        if (bot == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No bot with such id has been found"
            };

        if (bot.Player!.Id != playerId)
            return new NotBotCreatorError
            {
                Title = "NotBotCreatorError 400",
                Message = "You cannot register a bot, that you did not create"
            };


        if (await _tournamentRepository.IsRegisteredForTournament(botId, tournamentId))
            return new AlreadyRegisterForTournamentError
            {
                Title = "AlreadyRegisterForTournamentError 400",
                Message = "You are already registered for tournament"
            };

        var tournamentReference = new TournamentReference
        {
            tournamentId = tournamentId,
            botId = botId,
            LastModification = new DateTime()
        };

        if (!await _tournamentRepository.TournamentStarted(tournamentId))
            return new TournamentIsBeingPlayedError
            {
                Title = "TournamentIsBeingPlayedError 400",
                Message = "You cannot register a bot while the tournament is being played or has been finished"
            };

        await _tournamentRepository.AddTournamentReference(tournamentReference);
        await _tournamentRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId,
        long botId, long playerId)
    {
        return await _tournamentRepository.UnregisterSelfForTournament(tournamentId, botId, playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id,
        TournamentRequest tournamentRequest, long playerId)
    {
        if (tournamentRequest.Image != null && tournamentRequest.Image.Length % 4 != 0)
            return new IncorrectOperation
                { Message = "to nie jest string base 64 musi miec wielkosc podzielna przez 4" };

        return await _tournamentRepository.UpdateTournamentAsync(id, tournamentRequest, playerId);
    }


    private async Task<HandlerResult<Success, IErrorResult>> PlayerParticipate(long tourId, string? playerUsername)
    {
        if (playerUsername == null) return new Success();
        var res = await _tournamentRepository.GetParticipantName(tourId);
        foreach (var p in res)
            if (playerUsername.Equals(p))
                return new Success();

        return new EntityNotFoundErrorResult();
    }
}