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
}