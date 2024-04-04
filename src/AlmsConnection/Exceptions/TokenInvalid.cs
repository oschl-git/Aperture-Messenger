namespace ApertureMessenger.AlmsConnection.Exceptions;

public class TokenInvalid : Exception
{
    public TokenInvalid()
    {
    }

    public TokenInvalid(string message)
        : base(message)
    {
    }

    public TokenInvalid(string message, Exception inner)
        : base(message, inner)
    {
    }
}