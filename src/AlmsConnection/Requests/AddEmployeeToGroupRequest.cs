using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class AddEmployeeToGroupRequest : IRequest
{
    [JsonProperty("conversationId")] public int ConversationId;

    [JsonProperty("username")] public string Username; 
    
    [JsonConstructor]
    public AddEmployeeToGroupRequest(int conversationId, string username)
    {
        ConversationId = conversationId;
        Username = username;
    }
    
    public string GetRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}