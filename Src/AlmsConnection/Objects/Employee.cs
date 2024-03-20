namespace ApertureMessenger.AlmsConnection.Objects;

public class Employee
{
    public int Id;
    public string Username;
    public string Name;
    public string Surname;
    
    public Employee(string username, string name, string surname)
    {
        Username = username;
        Name = name;
        Surname = surname;
    }
}