using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IMatchService
{
    Task<HandlerResult<SuccessData<PageResponse<MatchResponse>>, IErrorResult>> GetListOfMatchesFiltered(
        MatchFilterRequest matchFilterRequest, PageParameters pageParameters);

    Task<HandlerResult<SuccessData<MatchResponse>, IErrorResult>> GetMatchById(long id);
    Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetLogFile(long matchId);
}