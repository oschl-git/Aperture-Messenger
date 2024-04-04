namespace ApertureMessenger.AlmsConnection.Exceptions;

public class BadRequestSent : Exception
{
    public BadRequestSent()
    {
    }

    public BadRequestSent(string message)
        : base(message)
    {
    }

    public BadRequestSent(string message, Exception inner)
        : base(message, inner)
    {
    }
}