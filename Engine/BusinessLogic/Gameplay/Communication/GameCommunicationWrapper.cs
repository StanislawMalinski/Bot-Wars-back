using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay.Interface;
using Newtonsoft.Json;

namespace Engine.BusinessLogic.Gameplay.Communication;

public class GameCommunicationWrapper
{
    private Dictionary<string, string> commands; 
    private readonly IExternalProgramSpeaker _externalProgramSpeaker;
    private readonly GameData _data;
    private readonly GameCommandsAccessor _accessor;

    public GameCommunicationWrapper(IExternalProgramSpeaker externalProgramSpeaker, GameData data, GameCommandsAccessor accessor)
    {
        _accessor = accessor;
        _data = data;
        _externalProgramSpeaker = externalProgramSpeaker;
        commands = _accessor.LoadCommandsFromJson();
    }
    
    public async void SetPosition(Position position)
    {
        List<string> positionList = new List<string>
        {
            position.position,
            position.side.ToString()
        };
        var command = _accessor.GetCommandString(_data.Game.GameFile!, commands["SetPosition"], positionList);
        await _externalProgramSpeaker.Send(command);
    }
    
    public async void PerformMove(Move move)
    {
        List<string> moveList = new List<string>
        {
            move.x.ToString(),
            move.y.ToString()
        };
        var command = _accessor.GetCommandString(_data.Game.GameFile!, commands["PerformMove"], moveList);
        await _externalProgramSpeaker.Send(command);
    }

    public async Task<Position> GetPosition()
    {
        var command = _accessor.GetCommandString(_data.Game.GameFile!, commands["GetPosition"],"");
        var output = await _externalProgramSpeaker.Send(command);
        string[] parts = output?.Split(' ')!;
        
        if (parts.Length == 2 && int.TryParse(parts[1], out int side))
        {
            Position position = new Position
            {
                position = parts[0],
                side = side
            };

            return position;
        }
        else
        {
            throw new Exception($"Error with communication occured!!!");
        }
    }
    
    public async Task<List<Move>> GetMoves(int side)
    {
        var command = _accessor.GetCommandString(_data.Game.GameFile!, commands["GetMoves"], side.ToString());
        var output = await _externalProgramSpeaker.Send(command);
        string[] parts = output?.Split(' ')!;
        List<Move> moves = new List<Move>();
        for (int i = 1; i < parts.Length; i+=2)
        {
            if (int.TryParse(parts[i-1], out int x) && int.TryParse(parts[i], out int y) && parts.Length % 2 == 0)
            {
                Move move = new Move
                {
                    x = x,
                    y = y
                };
                moves.Add(move);
            }else
            {
                throw new Exception($"Error with communication occured!!!");
            }
        }

        return moves;
    }

    public async void ResetPosition()
    {
        List<string> positionList = new List<string>
        {
            "Default position placeholder",
            "Default side placeholder"
        };// TODO
        var command = _accessor.GetCommandString(_data.Game.GameFile!, commands["SetPosition"], positionList);
        await _externalProgramSpeaker.Send(command);
    }
    



}