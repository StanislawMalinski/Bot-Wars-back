﻿using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPlayerRepository
{
    public Task<HandlerResult<SuccessData<string>, IErrorResult>> GenerateJwt(LoginDto dto);
    public Task<HandlerResult<Success, IErrorResult>> CreatePlayerAsync(PlayerDto playerDto);
    public Task<HandlerResult<Success, IErrorResult>> ChangePassword(ChangePasswordRequest password,long userId);
    public Task<HandlerResult<Success, IErrorResult>> DeletePlayerAsync(long id);
    public Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> GetPlayerAsync(long id);

    public Task<HandlerResult<SuccessData<List<PlayerDto>>, IErrorResult>> GetPlayersAsync();
    public Task<HandlerResult<SuccessData<PlayerInfo>, IErrorResult>> GetPlayerInfoAsync(long playerId);
    
}