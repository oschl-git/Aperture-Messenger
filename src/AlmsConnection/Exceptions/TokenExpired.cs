namespace ApertureMessenger.AlmsConnection.Exceptions;

public class TokenExpired : Exception
{
    public TokenExpired()
    {
    }

    public TokenExpired(string message)
        : base(message)
    {
    }
    
    public TokenExpired(string message, Exception inner)
        : base(message, inner)
    {
    }
}