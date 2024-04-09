namespace ApertureMessenger.AlmsConnection.Exceptions;

public class EmployeeAlreadyInConversation : Exception
{
    public EmployeeAlreadyInConversation()
    {
    }

    public EmployeeAlreadyInConversation(string message)
        : base(message)
    {
    }

    public EmployeeAlreadyInConversation(string message, Exception inner)
        : base(message, inner)
    {
    }
}