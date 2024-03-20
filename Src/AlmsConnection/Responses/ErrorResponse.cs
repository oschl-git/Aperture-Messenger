using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Responses;

[Serializable]
public class ErrorResponse
{
    [JsonProperty("error")]
    public int Error;
    
    [JsonProperty("message")]
    public string Message;

    [JsonConstructor]
    public ErrorResponse(int error, string message)
    {
        Error = error;
        Message = message;
    }
}