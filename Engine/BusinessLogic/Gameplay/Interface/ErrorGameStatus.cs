namespace Engine.BusinessLogic.Gameplay.Interface;

public enum ErrorGameStatus
{
    GameFileNotFound,
    GameCannotBeRun,
    GameExited,
    InvalidGameOutput,
    GameTimedOut,
    InternalError,
    MemoryLimit,
    TimeLimit
}