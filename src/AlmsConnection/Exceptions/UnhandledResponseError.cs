namespace ApertureMessenger.AlmsConnection.Exceptions;

public class UnhandledResponseError : Exception
{
    public UnhandledResponseError()
    {
    }

    public UnhandledResponseError(string message)
        : base(message)
    {
    }
}