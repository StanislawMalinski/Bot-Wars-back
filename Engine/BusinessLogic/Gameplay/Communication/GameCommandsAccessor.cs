using Newtonsoft.Json;

namespace Engine.BusinessLogic.Gameplay.Communication;

public class GameCommandsAccessor
{
    public string GetCommandString(string executable, string commandName, string argument)
    {
        return $"./{executable} {commandName} {argument}";
    }

    public string GetCommandString(string executable, string commandName, List<string> arguments)
    {
        string argumentString = string.Join(" ", arguments);
        return $"./{executable} {commandName} {argumentString}";
    }
    
    public Dictionary<string, string> LoadCommandsFromJson()
    {

        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine("Engine", "BusinessLogic", "BackgroundWorkers", "Data", "GameComands.json");
        string fullPath = Path.Combine(currentDirectory, relativePath);
        string jsonContent = File.ReadAllText(fullPath);
        
        Dictionary<string, string> commands = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

        return commands;
    }
}