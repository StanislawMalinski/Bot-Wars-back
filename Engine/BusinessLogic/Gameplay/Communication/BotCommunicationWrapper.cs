using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay.Communication;

public class BotCommunicationWrapper
{
    private Dictionary<string, string> commands;
    private readonly IExternalProgramSpeaker _externalProgramSpeaker;
    private readonly GameCommandsAccessor _accessor;
    private readonly Bot _bot;

    public BotCommunicationWrapper(IExternalProgramSpeaker externalProgramSpeaker, Bot bot,
        GameCommandsAccessor accessor)
    {
        _bot = bot;
        _accessor = accessor;
        _externalProgramSpeaker = externalProgramSpeaker;
        commands = _accessor.LoadCommandsFromJson();
    }

    public async void ResetBot()
    {
        var command = _accessor.GetCommandString(_bot.BotFile, commands["ResetBot"], "");
        await _externalProgramSpeaker.Send(command);
    }

    public async Task<Move> GetMove(Position position, List<Move> moves)
    {
        List<string> inputList = new List<string>
        {
            position.position,
            position.side.ToString()
        };
        foreach (var move in moves)
        {
            inputList.Add(move.x.ToString());
            inputList.Add(move.y.ToString());
        }

        var command = _accessor.GetCommandString(_bot.BotFile, commands["GetMove"], inputList);
        var output = await _externalProgramSpeaker.Send(command);
        string[] parts = output?.Split(' ')!;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y) && parts.Length == 2)
        {
            Move move = new Move
            {
                x = x,
                y = y
            };
            return move;
        }
        else
        {
            throw new Exception($"Error with communication occured!!!");
        }
    }
}