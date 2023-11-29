﻿using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IGameRepository
{
    public Task<ServiceResponse<Game>> CreateGameType(Game game);
    public Task<ServiceResponse<List<Game>>> GetGameTypes();
    public Task<ServiceResponse<Game>> DeleteGame(long id);
    public Task<ServiceResponse<Game>> GetGameType(long id);
    public Task<ServiceResponse<Game>> ModifyGameType(long id, Game game);
    
}