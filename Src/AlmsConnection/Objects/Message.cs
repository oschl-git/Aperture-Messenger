namespace ApertureMessenger.AlmsConnection.Objects;

public class Message
{
    public int Id;
    public Employee Employee;
    public string Content;
    public DateTime DateTimeSent;

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
}