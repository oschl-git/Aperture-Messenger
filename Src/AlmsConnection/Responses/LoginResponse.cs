using System.Text.Json.Serialization;
using ApertureMessenger.AlmsConnection.Objects;

namespace ApertureMessenger.AlmsConnection.Responses;

public class LoginResponse
{
    public string Token;
    public Employee Employee;

    [JsonConstructor]
    public LoginResponse(string token, Employee employee)
    {
        Token = token;
        Employee = employee;
    }
}