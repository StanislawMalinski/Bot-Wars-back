using System.Data;

namespace Engine.BusinessLogic.Gameplay.Interface;

public interface IExternalProgramSpeaker
{
    public void SetConstrain(Constraint constraint); // TODO
    public Task<String?> Send(String messageData);
}