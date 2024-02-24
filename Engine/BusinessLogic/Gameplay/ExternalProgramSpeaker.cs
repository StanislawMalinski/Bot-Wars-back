using System.Data;
using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.Gameplay;

public class ExternalProgramSpeaker : IExternalProgramSpeaker
{
    private readonly ICorespondable _corespondable;

    public ExternalProgramSpeaker(ICorespondable corespondable)
    {
        _corespondable = corespondable;
    }


    public void SetConstrain(Constraint constraint)
    {
        // TODO
    }

    public async Task<String?> Send(string messageData)
    {
        return await _corespondable.Send(messageData);
    }
}