namespace ApertureMessenger.AlmsConnection.Objects;

public class Conversation
{
    public int Id;
    public string? Name;
    public bool IsGroup;
    public DateTime DateTimeCreated;
    public DateTime DateTimeUpdated;
    public List<Employee>? Participants;

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
}