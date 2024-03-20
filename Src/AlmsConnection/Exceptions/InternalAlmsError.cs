namespace ApertureMessenger.AlmsConnection.Exceptions;

public class InternalAlmsError : Exception
{
    public InternalAlmsError()
    {
    }

    public InternalAlmsError(string message)
        : base(message)
    {
    }
}