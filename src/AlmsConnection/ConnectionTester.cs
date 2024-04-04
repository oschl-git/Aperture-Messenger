using System.Net;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.Logic;

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
        var response = Connector.Get("/", true);
        return response.StatusCode == HttpStatusCode.OK;
    }

    public static VersionConflictResult CompareMessengerAndAlmsVersions(string targetVersion, string actualVersion)
    {
        var result = AlmsVersionComparer.CompareAlmsVersions(targetVersion, actualVersion);
        return new VersionConflictResult(targetVersion, actualVersion, result);
    }
}