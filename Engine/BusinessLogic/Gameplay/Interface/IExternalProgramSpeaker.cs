using System.Data;

namespace Engine.BusinessLogic.Gameplay.Interface;

public interface IExternalProgramSpeaker
{
    public void SetConstrain(Constraint constraint); // TODO
    public string Send(string messageData);
}