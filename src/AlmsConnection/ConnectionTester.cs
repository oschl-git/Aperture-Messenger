using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection;

/// <summary>
/// Allows testing whether ALMS connection is available.
/// </summary>
public static class ConnectionTester
{
    /// <summary>
    /// Tests connection to ALMS.
    /// </summary>
    /// <returns>Whether an 200 OK code was received.</returns>
    public static bool TestConnection()
    {
        var response = Connector.Get(
            "/"
        );

        return response.StatusCode == HttpStatusCode.OK;
    }

    public static StatusResponse GetAlmsStatus()
    {
        var response = Connector.Get("/");

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var contentString = ResponseParser.GetResponseContent(response);

                StatusResponse? status;
                try
                {
                    status = JsonConvert.DeserializeObject<StatusResponse>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing direct conversation JSON");
                }

                if (status == null) throw new JsonException("Direct conversation JSON was empty");

                return status;
        }

        throw new UnhandledResponseError();
    }

    
}