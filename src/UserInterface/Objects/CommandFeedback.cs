namespace ApertureMessenger.UserInterface.Objects;

public class CommandFeedback
{
    public enum FeedbackType
    {
        Success,
        Error,
        Warning,
        Info,
        Loading
    }

    public string Response;
    public FeedbackType Type;

    public CommandFeedback(string response, FeedbackType type)
    {
        Response = response;
        Type = type;
    }

    public string GetTypeSymbol()
    {
        return Type switch
        {
            FeedbackType.Success => "\u2713",
            FeedbackType.Error => "\ud83d\uddd9",
            FeedbackType.Warning => "\u26a0",
            FeedbackType.Info => "\ud83d\udec8",
            FeedbackType.Loading => "\u27f3",
            _ => "?"
        };
    }

    public ConsoleColor GetTypeConsoleColor()
    {
        return Type switch
        {
            FeedbackType.Success => ConsoleColor.Green,
            FeedbackType.Error => ConsoleColor.Red,
            FeedbackType.Warning => ConsoleColor.Yellow,
            FeedbackType.Info => ConsoleColor.DarkCyan,
            FeedbackType.Loading => ConsoleColor.Gray,
            _ => ConsoleColor.Black
        };
    }

    public override string ToString()
    {
        return Response;
    }
}