﻿using Shared.DataAccess.DAO;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Player;

public class PlayerServiceProvider
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerServiceProvider(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<HandlerResult<SuccessData<PlayerDto>, IErrorResult>> getPlayerInfo(long PlayerId)
    {
        return await _playerRepository.GetPlayerAsync(PlayerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> registerNewPlayer(PlayerDto PlayerModel)
    {
        return await _playerRepository.CreatePlayerAsync(PlayerModel);
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByLogin(String Login)
    {
        return new NotImplementedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> resetPassWordByEmail(String Email)
    {
        return new NotImplementedError();
    }
    
   

}