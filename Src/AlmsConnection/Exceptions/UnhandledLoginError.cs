namespace ApertureMessenger.AlmsConnection.Exceptions;

public class UnhandledLoginError : Exception
{
    public UnhandledLoginError()
    {
    }

    public UnhandledLoginError(string message)
        : base(message)
    {
    }
}