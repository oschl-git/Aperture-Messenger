using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class RegisterRequest : IRequest
{
    [JsonProperty("username")] public string Username;

    [JsonProperty("name")] public string Name;

    [JsonProperty("surname")] public string Surname;

    [JsonProperty("password")] public string Password;

    [JsonConstructor]
    public RegisterRequest(string username, string name, string surname, string password)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Password = password;
    }

    public string GetRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}