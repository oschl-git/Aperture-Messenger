namespace ApertureMessenger.AlmsConnection.Exceptions;

public class TooManyRequests : Exception
{
    public TooManyRequests()
    {
    }

    public TooManyRequests(string message)
        : base(message)
    {
    }
}