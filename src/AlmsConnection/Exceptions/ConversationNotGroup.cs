namespace ApertureMessenger.AlmsConnection.Exceptions;

public class ConversationNotGroup : Exception
{
    public ConversationNotGroup()
    {
    }

    public ConversationNotGroup(string message)
        : base(message)
    {
    }

    public ConversationNotGroup(string message, Exception inner)
        : base(message, inner)
    {
    }
}