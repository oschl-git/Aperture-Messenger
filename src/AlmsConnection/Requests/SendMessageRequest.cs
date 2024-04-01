using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class SendMessageRequest : IRequest
{
    [JsonProperty("conversationId")]
    public int ConversationId;
    
    [JsonProperty("content")]
    public string Content;

    [JsonConstructor]
    public SendMessageRequest(int conversationId, string content)
    {
        ConversationId = conversationId;
        Content = content;
    }

    public string getRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}