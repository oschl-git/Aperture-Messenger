namespace ApertureMessenger.AlmsConnection.Exceptions;

public class UnhandledAuthenticationError : Exception
{
    public UnhandledAuthenticationError()
    {
    }

    public UnhandledAuthenticationError(string message)
        : base(message)
    {
    }

    public UnhandledAuthenticationError(string message, Exception inner)
        : base(message, inner)
    {
    }
}