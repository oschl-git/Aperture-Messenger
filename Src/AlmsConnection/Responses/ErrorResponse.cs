using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Responses;

[Serializable]
public class ErrorResponse
{
    [JsonProperty("error")]
    public int Error;
    
    [JsonProperty("message")]
    public string Message;

    [JsonProperty("errors")]
    public List<string>? Errors;

    [JsonConstructor]
    public ErrorResponse(int error, string message, List<string>? errors = null)
    {
        Error = error;
        Message = message;
        Errors = errors;
    }
}