using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Objects;

[Serializable]
public class Message
{
    [JsonProperty("id")] public int Id;

    public Employee Employee;

    [JsonProperty("content")] public string Content;

    [JsonProperty("datetimeSent")] public DateTime DateTimeSent;

    [JsonConstructor]
    public Message(
        int id,
        int employeeId,
        string username,
        string name,
        string surname,
        string content,
        DateTime dateTimeSent
    )
    {
        Id = id;
        Employee = new Employee(employeeId, username, name, surname);
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