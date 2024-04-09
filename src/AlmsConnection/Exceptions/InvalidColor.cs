namespace ApertureMessenger.AlmsConnection.Exceptions;

public class InvalidColor : Exception
{
    public InvalidColor()
    {
    }

    public InvalidColor(string message)
        : base(message)
    {
    }

    public InvalidColor(string message, Exception inner)
        : base(message, inner)
    {
    }
}