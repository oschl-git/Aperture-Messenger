namespace ApertureMessenger.AlmsConnection;

public sealed class Connector
{
    private static HttpClient AlmsClient;
    
    private static readonly Connector Instance = new();

    private Connector()
    {
        AlmsClient = new HttpClient
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };
    }

    public static Connector get()
    {
        return Instance;
    }
}