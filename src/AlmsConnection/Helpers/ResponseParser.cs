namespace ApertureMessenger.AlmsConnection.Helpers;

public static class ResponseParser
{
    public static string GetResponseContent(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        return reader.ReadToEnd();
    }
}