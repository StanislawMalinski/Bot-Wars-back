using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using MMatches = Shared.DataAccess.DataBaseEntities.Matches;

namespace Communication.Services.Matches;

public class MatchService : IMatchService
{
    private readonly IFileRepository _fileRepository;
    private readonly MatchMapper _matchMapper;
    private readonly MatchRepository _matchRepository;

    public MatchService(MatchRepository matchRepository, MatchMapper matchMapper, IFileRepository fileRepository)
    {
        _matchRepository = matchRepository;
        _matchMapper = matchMapper;
        _fileRepository = fileRepository;
    }

    public async Task<HandlerResult<SuccessData<PageResponse<MatchResponse>>, IErrorResult>> GetListOfMatchesFiltered(
        MatchFilterRequest matchFilterRequest, PageParameters pageParameters)
    {
        var unfilteredMatches = _matchRepository.GetListOfUnfilteredMatches();

        if (matchFilterRequest.GameName != null && !matchFilterRequest.GameName.Equals(string.Empty))
            unfilteredMatches = unfilteredMatches.Where(match =>
                match.Game != null && match.Game.GameFile == matchFilterRequest.GameName);

        if (matchFilterRequest.MinPlayOutDate != null)
            unfilteredMatches = unfilteredMatches.Where(match =>
                match.Played >= matchFilterRequest.MinPlayOutDate);

        if (matchFilterRequest.MaxPlayOutDate != null)
            unfilteredMatches = unfilteredMatches.Where(match =>
                match.Played <= matchFilterRequest.MaxPlayOutDate);

        if (matchFilterRequest.TournamentName != null && !matchFilterRequest.TournamentName.Equals(string.Empty))
            unfilteredMatches = unfilteredMatches.Where(match =>
                match.Tournament != null && match.Tournament.TournamentTitle == matchFilterRequest.TournamentName);
        if (matchFilterRequest.userParticipation != null && !matchFilterRequest.userParticipation.Equals(string.Empty))
            unfilteredMatches = unfilteredMatches.Where(match =>
                match.MatchPlayers!.Any(mp => mp.Bot.Player.Login.Equals(matchFilterRequest.userParticipation)));
        //XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
        /*if (matchFilterRequest.Username != null)
        {
            unfilteredMatches = unfilteredMatches.Where(match => match
                .MatchPlayers != null && !match
                .MatchPlayers.Where(matchPlayers => matchPlayers.Bot != null && matchPlayers.Bot.Player != null
                                                                             && matchPlayers.Bot != null && matchPlayers.Bot.Player.Login
                                                                             ==matchFilterRequest.Username).ToList().IsNullOrEmpty());
        }*/
        int cout = unfilteredMatches.Count();
        var matches = await unfilteredMatches
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(match => _matchMapper.MapEntityToResponse(match))
            .ToListAsync();

        return new SuccessData<PageResponse<MatchResponse>>
        {
            Data = new PageResponse<MatchResponse>(matches,pageParameters.PageSize, cout)
        };
    }

    public async Task<HandlerResult<SuccessData<MatchResponse>, IErrorResult>> GetMatchById(long id)
    {
        var match = await _matchRepository.GetMatchByIdExtended(id);

        if (match == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "Match could not have been found"
            };

        return new SuccessData<MatchResponse>
        {
            Data = _matchMapper.MapEntityToResponse(match)
        };
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetLogFile(long matchId)
    {
        var match = await _matchRepository.GetMatchById(matchId);

        if (match == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "Match could not have been found"
            };

        var log = await _fileRepository.GetFile(match.LogId, CreateLogName(match));

        if (!log.IsSuccess)
        {
            Console.WriteLine("Log file not found in File Gatherer");
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "Log not found in File Gatherer"
            };
        }

        return new SuccessData<IFormFile>
        {
            Data = log.Match(x => x.Data, null!)
        };
    }

    private string CreateLogName(MMatches match)
    {
        return "tournament" +
               match.TournamentsId +
               "game" +
               match.GameId +
               "log" +
               match.Played.ToString("yyyy_MM_dd");
    }
}