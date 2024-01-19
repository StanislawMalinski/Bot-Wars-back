﻿using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerBadValidation : PlayerUnidentifiedPlayerService , IPlayerService
{
    public PlayerBadValidation(PlayerServiceProvider playerServiceProvider) : base(playerServiceProvider)
    {
        
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> registerNewPlayer(PlayerDto PlayerModel)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(string Email)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(string Login)
    {
        return new AccessDeniedError();
    }
}