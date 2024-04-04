namespace ApertureMessenger.AlmsConnection.Exceptions;

public class TokenMissing : Exception
{
    public TokenMissing()
    {
    }

    public TokenMissing(string message)
        : base(message)
    {
    }

    public TokenMissing(string message, Exception inner)
        : base(message, inner)
    {
    }
}