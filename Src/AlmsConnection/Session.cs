using ApertureMessenger.AlmsConnection.Objects;

namespace ApertureMessenger.AlmsConnection;

public sealed class Session
{
    public string? Token;
    public Employee? Employee;
    
    private static readonly Session Instance = new();

    private Session()
    {
    }

    public static Session get()
    {
        return Instance;
    }
    
    public void setParameters(string token, Employee employee)
    {
        Token = token;
        Employee = employee;
    }
}