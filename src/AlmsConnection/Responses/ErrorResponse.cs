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
    
    [JsonProperty("employees")]
    public List<string>? Usernames;

    [JsonConstructor]
    public ErrorResponse(int error, string message, List<string>? errors = null, List<string>? usernames = null)
    {
        Error = error;
        Message = message;
        Errors = errors;
        Usernames = usernames;
    }
}