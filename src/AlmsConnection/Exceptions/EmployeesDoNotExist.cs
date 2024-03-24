namespace ApertureMessenger.AlmsConnection.Exceptions;

public class EmployeesDoNotExist : Exception
{
    public string[] Usernames;
    
    public EmployeesDoNotExist()
    {
    }
    
    public EmployeesDoNotExist(string[] usernames)
    {
        Usernames = usernames;
    }

    public EmployeesDoNotExist(string message)
        : base(message)
    {
    }

    public EmployeesDoNotExist(string message, Exception inner)
        : base(message, inner)
    {
    }
}