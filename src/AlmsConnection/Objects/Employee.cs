using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Objects;

[Serializable]
public class Employee
{
    [JsonProperty("id")] public int Id;

    [JsonProperty("username")] public string Username;

    [JsonProperty("name")] public string Name;

    [JsonProperty("surname")] public string Surname;

    [JsonProperty("color")] public int? Color;

    [JsonConstructor]
    public Employee(int id, string username, string name, string surname, int? color)
    {
        Id = id;
        Username = username;
        Name = name;
        Surname = surname;
        Color = color;
    }

    public override string ToString()
    {
        return $"Employee ID: {Id}\n" +
               $"Username: {Username}\n" +
               $"Name: {Name}\n" +
               $"Surname: {Surname}" +
               $"Color: {Color}";
    }
}