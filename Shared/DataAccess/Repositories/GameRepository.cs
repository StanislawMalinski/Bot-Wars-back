using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;
    private readonly IGameTypeMapper _mapper;

    public GameRepository(DataContext dataContext, IGameTypeMapper gameTypeMapper)
    {
        _mapper = gameTypeMapper;
        _dataContext = dataContext;
    }


    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(GameDto game)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(gameRequest.GameFile.OpenReadStream());
            content.Add(streamContent, "file", gameRequest.GameFile.FileName);
            var res = await _httpClient.PutAsync(_gathererEndpoint, content);
            long gameFileId;
            if (res.IsSuccessStatusCode)
            {
                string cont = await res.Content.ReadAsStringAsync();
                gameFileId = Convert.ToInt32(cont);
            }
            else
            {
                return new IncorrectOperation
                {
                    Title = "IncorrectOperation 404",
                    Message = "Faild to upload file to FileGatherer"
                };
            }
            Game game = _mapper.MapRequestToGame(gameRequest);
            game.FileId = gameFileId;
            await _dataContext
                .Games
                .AddAsync(game);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }
        catch (Exception ex)
        {
            return new IncorrectOperation
            {
                Title = "IncorrectOperation 505",
                Message = ex.Message
            };
        }
=======
        
        var resGame = await _dataContext.Games.AddAsync(_mapper.ToGameType(game));
        await _dataContext.SaveChangesAsync();
        return new Success();
>>>>>>> parent of 82c6fa8 (Merge pull request #91 from StanislawMalinski/eloHell)
=======
        
        await _dataContext
            .Games
            .AddAsync(_mapper.MapRequestToGame(gameRequest));
        
        await _dataContext.SaveChangesAsync();
        return new Success();
>>>>>>> parent of e4c6155 (Integrated adding files to FileGatherer with api)

    }

    public async Task<HandlerResult<SuccessData<List<GameDto>>, IErrorResult>> GetGameTypes()
    {
        var resGame = _dataContext.Games.Select(x => _mapper.ToDto(x)).ToList();
        
        return new SuccessData<List<GameDto>>()
        {
            Data = resGame
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        
        var gameToRemove = await _dataContext
            .Games
            .Include(g => g.Tournaments)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (gameToRemove == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No such element could have been found"
            };
        }

        if (gameToRemove.Tournaments != null) _dataContext.Tournaments.RemoveRange(gameToRemove.Tournaments);
        _dataContext.Games.Remove(gameToRemove);
        await _dataContext.SaveChangesAsync();
        return new Success();


    }

    public async Task<HandlerResult<SuccessData<GameDto>, IErrorResult>> GetGameType(long id)
    {
        
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie znaleziono elementu o tym id"
            };
        }

        return new SuccessData<GameDto>()
        {
            Data = _mapper.ToDto(resGame)
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameDto gameDto)
    {
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Nie ma elemętu w bazie danych"
            };
        }
        resGame.InterfaceDefinition = gameDto.InterfaceDefinition;
        resGame.GameInstructions = gameDto.GameInstructions;
        resGame.GameFile = gameDto.GameFile;
        resGame.NumbersOfPlayer = gameDto.NumbersOfPlayer;
        resGame.LastModification = DateTime.Now;
        resGame.IsAvailableForPlay = gameDto.IsAvaiableForPlay;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
}