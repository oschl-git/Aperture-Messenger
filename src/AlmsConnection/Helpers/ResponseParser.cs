using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Helpers;

public static class ResponseParser
{
    public static string GetResponseContent(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        return reader.ReadToEnd();
    }

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

        if (errorContent == null)
        {
            throw new JsonException("JSON error response was empty");
        }

        return errorContent;
    }
}