using ApertureMessenger.AlmsConnection.Objects;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Responses;

[Serializable]
public class LoginResponse
{
    [JsonProperty("token")]
    public string Token;
    
    [JsonProperty("employee")]
    public Employee Employee;

    [JsonConstructor]
    public LoginResponse(string token, Employee employee)
    {
        Token = token;
        Employee = employee;
    }
}