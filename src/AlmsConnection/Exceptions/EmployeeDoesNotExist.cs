namespace ApertureMessenger.AlmsConnection.Exceptions;

public class EmployeeDoesNotExist : Exception
{
    public EmployeeDoesNotExist()
    {
    }

    public EmployeeDoesNotExist(string message)
        : base(message)
    {
    }

    public EmployeeDoesNotExist(string message, Exception inner)
        : base(message, inner)
    {
    }
}