using System.Configuration;
using System.Net;
using System.Text;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;

namespace ApertureMessenger.AlmsConnection;

/// <summary>
/// A singleton that handles sending requests to ALMS.
/// </summary>
public sealed class Connector
{
    private readonly HttpClient _almsClient;

    private static readonly Connector Instance = new();

    private Connector()
    {
        var url = ConfigurationManager.AppSettings.Get("AlmsUrl");

        if (url == null) throw new ConfigurationErrorsException("ALMS URL is not properly configured");

        _almsClient = new HttpClient
        {
            BaseAddress = new Uri(url)
        };
    }

    public static Connector GetInstance()
    {
        return Instance;
    }

    /// <summary>
    /// Sends a GET HTTP request to ALMS.
    /// </summary>
    /// <param name="endpoint">Endpoint to send the request to</param>
    /// <param name="disableAuthorization">Disables sending auth headers and reading auth errors</param>
    /// <returns>ALMS response</returns>
    public static HttpResponseMessage Get(
        string endpoint,
        bool disableAuthorization = false
    )
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

        if (!disableAuthorization) AddAuthorizationHeaders(request);

        var response = SendRequest(request);

        ThrowRateLimitError(response);

        if (!disableAuthorization) ThrowAuthorizationErrors(response);

        return response;
    }

    /// <summary>
    /// Sends a POST HTTP request to ALMS.
    /// </summary>
    /// <param name="endpoint">Endpoint to send the request to</param>
    /// <param name="content">Content to include in the request</param>
    /// <param name="disableAuthorization">Disables sending auth headers and reading auth errors</param>
    /// <returns>ALMS response</returns>
    public static HttpResponseMessage Post(
        string endpoint,
        string content,
        bool disableAuthorization = false
    )
    {
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        if (!disableAuthorization) AddAuthorizationHeaders(request);

        var response = SendRequest(request);

        ThrowRateLimitError(response);

        if (!disableAuthorization) ThrowAuthorizationErrors(response);

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
        request.Headers.Add("Token", Session.Token);
    }

    private static void ThrowAuthorizationErrors(HttpResponseMessage response)
    {
        if (response.StatusCode != HttpStatusCode.Unauthorized) return;

        var errorResponse = ResponseParser.GetErrorResponse(response);
        switch (errorResponse.Message)
        {
            case "UNAUTHORIZED":
                throw new TokenMissing();

            case "AUTH TOKEN BAD":
                throw new TokenInvalid();

            case "AUTH TOKEN EXPIRED":
                throw new TokenExpired();
        }
    }

    private static void ThrowRateLimitError(HttpResponseMessage response)
    {
        if (response.StatusCode != HttpStatusCode.TooManyRequests) return;

        throw new TooManyRequests();
    }
}