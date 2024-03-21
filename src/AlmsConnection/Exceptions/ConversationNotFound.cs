namespace ApertureMessenger.AlmsConnection.Exceptions;

public class ConversationNotFound : Exception
{
    public ConversationNotFound()
    {
    }

    public ConversationNotFound(string message)
        : base(message)
    {
    }

    public ConversationNotFound(string message, Exception inner)
        : base(message, inner)
    {
    }
}