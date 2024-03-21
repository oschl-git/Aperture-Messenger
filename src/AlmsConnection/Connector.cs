using System.Configuration;
using System.Net;
using System.Text;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection;

public sealed class Connector
{
    private readonly HttpClient _almsClient;
    
    private static readonly Connector Instance = new();

    private Connector()
    {
        var url = ConfigurationManager.AppSettings.Get("AlmsUrl");

        if (url == null)
        {
            throw new ConfigurationErrorsException("ALMS URL is not properly configured");
        }
        
        _almsClient = new HttpClient
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
        
        var response = SendRequest(request);
        
        ThrowAuthorizationErrors(response);

        return response;
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

        var response = SendRequest(request);
        
        ThrowAuthorizationErrors(response);

        return response;
    }

    private static HttpResponseMessage SendRequest(HttpRequestMessage request)
    {
        HttpResponseMessage response;
        try
        {
            response = GetInstance()._almsClient.Send(request);
        }
        catch (HttpRequestException)
        {
            throw new FailedContactingAlms();
        }

        return response;
    }

    private static void AddAuthorizationHeaders(HttpRequestMessage request)
    {
        request.Headers.Add("Token", Session.GetInstance().Token);
    }

    private static void ThrowAuthorizationErrors(HttpResponseMessage response)
    {
        if (response.StatusCode != HttpStatusCode.Unauthorized) return;

        var contentString = ResponseParser.GetResponseContent(response);
        
        ErrorResponse? errorContent;
        try
        {
            errorContent = JsonConvert.DeserializeObject<ErrorResponse>(contentString);
        }
        catch (Exception)
        {
            throw new JsonException("Failed parsing JSON auth error response");
        }

        if (errorContent == null)
        {
            throw new JsonException("JSON login auth error response was empty");
        }

        switch (errorContent.Message)
        {
            case "UNAUTHORIZED":
                throw new TokenMissing();
            
            case "AUTH TOKEN BAD":
                throw new TokenInvalid();
            
            case "AUTH TOKEN EXPIRED":
                throw new TokenExpired();
        }
    }
}