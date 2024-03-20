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

    public static Session GetInstance()
    {
        return Instance;
    }
    
    public void SetParameters(string token, Employee employee)
    {
        Token = token;
        Employee = employee;
    }
}