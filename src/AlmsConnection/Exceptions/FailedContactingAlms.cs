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
    
    public FailedContactingAlms(string message, Exception inner)
        : base(message, inner)
    {
    }
}