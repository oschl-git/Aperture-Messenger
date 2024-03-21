namespace ApertureMessenger.AlmsConnection.Exceptions;

public class MessageContentWasTooLong : Exception
{
    public MessageContentWasTooLong()
    {
    }

    public MessageContentWasTooLong(string message)
        : base(message)
    {
    }

    public MessageContentWasTooLong(string message, Exception inner)
        : base(message, inner)
    {
    }
}