using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Helpers;

/// <summary>
/// Handles parsing responses.
/// </summary>
public static class ResponseParser
{
    /// <summary>
    /// Returns the response content as a string.
    /// </summary>
    /// <param name="response">The response to parse</param>
    /// <returns>String response content</returns>
    public static string GetResponseContent(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Returns the content of an error response.
    /// </summary>
    /// <param name="response">The response to parse</param>
    /// <returns>Error response content</returns>
    public static ErrorResponse GetErrorResponse(HttpResponseMessage response)
    {
        var contentString = GetResponseContent(response);

        ErrorResponse? errorContent;
        try
        {
            errorContent = JsonConvert.DeserializeObject<ErrorResponse>(contentString);
        }
        catch (Exception)
        {
            throw new JsonException("Failed parsing JSON error response");
        }

        if (errorContent == null) throw new JsonException("JSON error response was empty");

        return errorContent;
    }
}