using System.Configuration;
using System.Text;

namespace ApertureMessenger.AlmsConnection;

public sealed class Connector
{
    private static HttpClient AlmsClient;
    
    private static readonly Connector Instance = new();

    private Connector()
    {
        var url = ConfigurationManager.AppSettings.Get("AlmsUrl") ?? "http://127.0.0.1:5678/";
        if (url == null)
        {
            throw new ConfigurationErrorsException("ALMS URL is not configured.");
        }
        
        AlmsClient = new HttpClient
        {
            BaseAddress = new Uri(url)
        };
    }

    public static Connector GetInstance()
    {
        return Instance;
    }

    public static HttpResponseMessage Get(string endpoint, bool disableAuthorizationHeaders = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        
        if (!disableAuthorizationHeaders)
        {
            AddAuthorizationHeaders(request);
        }
        
        return AlmsClient.Send(request);
    }
    
    public static HttpResponseMessage Post(string endpoint, string content, bool disableAuthorizationHeaders = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
        
        if (!disableAuthorizationHeaders)
        {
            AddAuthorizationHeaders(request);
        }
        
        return AlmsClient.Send(request);
    }

    private static void AddAuthorizationHeaders(HttpRequestMessage request)
    {
        request.Headers.Add("Token", Session.GetInstance().Token);
    }
}