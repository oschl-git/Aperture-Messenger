using System.Text.Json.Serialization;

namespace ApertureMessenger.AlmsConnection.Objects;

public class Employee
{
    public int Id;
    public string Username;
    public string Name;
    public string Surname;
    
    [JsonConstructor]
    public Employee(int id, string username, string name, string surname)
    {
        Id = id;
        Username = username;
        Name = name;
        Surname = surname;
    }
}