using System.Net;

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
}