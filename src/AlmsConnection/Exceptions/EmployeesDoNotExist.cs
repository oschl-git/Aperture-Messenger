namespace ApertureMessenger.AlmsConnection.Exceptions;

public class EmployeesDoNotExist : Exception
{
    public string[] Usernames;

    public EmployeesDoNotExist(string[] usernames)
    {
        Usernames = usernames;
    }

    public EmployeesDoNotExist(string[] usernames, string message)
        : base(message)
    {
        Usernames = usernames;
    }

    public EmployeesDoNotExist(string[] usernames, string message, Exception inner)
        : base(message, inner)
    {
        Usernames = usernames;
    }
}