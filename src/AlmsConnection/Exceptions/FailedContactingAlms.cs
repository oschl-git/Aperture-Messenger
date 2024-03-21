namespace ApertureMessenger.AlmsConnection.Exceptions;

public class FailedContactingAlms : Exception
{
    public FailedContactingAlms()
    {
    }

    public FailedContactingAlms(string message)
        : base(message)
    {
    }
}