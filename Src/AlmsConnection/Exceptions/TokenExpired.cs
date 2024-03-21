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
}