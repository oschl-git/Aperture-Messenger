using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class LoginRequest
{
    [JsonProperty("username")]
    public string Username;
    
    [JsonProperty("password")]
    public string Password;

    [JsonConstructor]
    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}