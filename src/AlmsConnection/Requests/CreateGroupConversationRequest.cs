using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class CreateGroupConversationRequest : IRequest
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("employees")]
    public string[] EmployeeUsernames;

    [JsonConstructor]
    public CreateGroupConversationRequest(string name, string[] employeeUsernames)
    {
        Name = name;
        EmployeeUsernames = employeeUsernames;
    }

    public string getRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}