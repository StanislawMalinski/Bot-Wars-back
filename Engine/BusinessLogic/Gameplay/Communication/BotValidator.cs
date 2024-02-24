using System.Data;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay.Communication;

public class BotValidator
{
    private readonly Bot _bot;

    public BotValidator(Bot bot)
    {
        _bot = bot;
    }

    public bool Validate()
    {
        return false; // TODO
    }
    public void SetConstrain(Constraint constraint)
    {
        // TODO
    }
}