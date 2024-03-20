namespace ApertureMessenger.AlmsConnection.Exceptions;

public class UserDoesNotExist : Exception
{
    public UserDoesNotExist()
    {
    }

    public UserDoesNotExist(string message)
        : base(message)
    {
    }
}