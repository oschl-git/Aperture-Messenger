using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class LoginRequest : Request
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

    public string getRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}