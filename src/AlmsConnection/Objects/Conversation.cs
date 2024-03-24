using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Objects;

[Serializable]
public class Conversation
{
    [JsonProperty("id")]
    public int Id;
    
    [JsonProperty("name")]
    public string? Name;
    
    [JsonProperty("isGroup")]
    public bool IsGroup;
    
    [JsonProperty("datetimeCreated")]
    public DateTime DateTimeCreated;
    
    [JsonProperty("datetimeUpdated")]
    public DateTime DateTimeUpdated;
    
    [JsonProperty("participants")]
    public List<Employee>? Participants;

    [JsonConstructor]
    public Conversation(
        int id,
        string? name,
        bool isGroup,
        DateTime dateTimeCreated,
        DateTime dateTimeUpdated,
        List<Employee>? participants
    )
    {
        Id = id;
        Name = name;
        IsGroup = isGroup;
        DateTimeCreated = dateTimeCreated;
        DateTimeUpdated = dateTimeUpdated;
        Participants = participants;
    }
    
    public override string ToString()
    {
        var participantList = Participants != null ? string.Join(", ", Participants.ConvertAll(p => p.Username)) : "No participants";

        return $"Conversation ID: {Id}\n" +
               $"Name: {Name ?? "No name provided"}\n" +
               $"Is Group: {IsGroup}\n" +
               $"Date Time Created: {DateTimeCreated}\n" +
               $"Date Time Updated: {DateTimeUpdated}\n" +
               $"Participants: {participantList}";
    }
}