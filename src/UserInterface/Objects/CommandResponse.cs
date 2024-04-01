namespace ApertureMessenger.UserInterface.Objects;

public class CommandResponse
{
    public enum ResponseType
    {
        Success,
        Error,
        Warning,
        Info,
        Loading
    }
    
    public string Response;
    public ResponseType Type;

    public CommandResponse(string response, ResponseType type)
    {
        Response = response;
        Type = type;
    }

    public string GetTypeSymbol()
    {
        return Type switch
        {
            ResponseType.Success => "\u2713",
            ResponseType.Error => "\ud83d\uddd9",
            ResponseType.Warning => "\u26a0",
            ResponseType.Info => "\ud83d\udec8",
            ResponseType.Loading => "\u27f3",
            _ => "?"
        };
    }

    public ConsoleColor GetTypeConsoleColor()
    {
        return Type switch
        {
            ResponseType.Success => ConsoleColor.Green,
            ResponseType.Error => ConsoleColor.Red,
            ResponseType.Warning => ConsoleColor.Yellow,
            ResponseType.Info => ConsoleColor.DarkCyan,
            ResponseType.Loading => ConsoleColor.Gray,
            _ => ConsoleColor.Black
        }; 
    }

    public override string ToString()
    {
        return Response;
    }
}