namespace ApertureMessenger.AlmsConnection;

public sealed class User
{
    public string Token;
    
    public string Username;
    public string Name;
    public string Surname;
    
    private static readonly User instance = new();

    private User()
    {
    }

    public static User Instance => instance;

    public void setParameters(string token, string username, string name, string surname)
    {
        Token = token;
        Username = username;
        Name = name;
        Surname = surname;
    }
}