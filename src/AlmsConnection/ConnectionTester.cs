using System.Net;

namespace ApertureMessenger.AlmsConnection;

public static class ConnectionTester
{
    public static bool TestConnection()
    {
        var response = Connector.Get(
            "/"
        );

        return response.StatusCode == HttpStatusCode.OK;
    }
}