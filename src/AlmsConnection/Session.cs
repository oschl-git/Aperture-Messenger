using ApertureMessenger.AlmsConnection.Objects;

namespace ApertureMessenger.AlmsConnection;

/// <summary>
/// Stores details about the current session.
/// </summary>
public static class Session
{
    public static string? Token { get; private set; }
    public static Employee? Employee;
    
    public static void SetSession(string token, Employee employee)
    {
        Token = token;
        Employee = employee;
    }

    public static void ClearSession()
    {
        Token = null;
        Employee = null;
    }
}