using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Objects;

[Serializable]
public class Message
{
    [JsonProperty("id")]
    public int Id;
    
    [JsonProperty("employee")]
    public Employee Employee;
    
    [JsonProperty("content")]
    public string Content;
    
    [JsonProperty("datetimeSent")]
    public DateTime DateTimeSent;

    [JsonConstructor]
    public Message(
        int id, 
        Employee employee,
        string content,
        DateTime dateTimeSent
        )
    {
        Id = id;
        Employee = employee;
        Content = content;
        DateTimeSent = dateTimeSent;
    }
    
    public override string ToString()
    {
        return $"Message ID: {Id}\n" +
               $"Employee: {Employee.Username}\n" +
               $"Content: {Content}\n" +
               $"Date Time Sent: {DateTimeSent}";
    }
}