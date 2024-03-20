namespace ApertureMessenger.AlmsConnection.Exceptions;

public class IncorrectPassword : Exception
{
    public IncorrectPassword()
    {
    }

    public IncorrectPassword(string message)
        : base(message)
    {
    }
}