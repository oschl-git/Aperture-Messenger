using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Responses;

[Serializable]
public class StatsResponse
{
    [JsonProperty("activeUsers")] public int ActiveUsers;

    [JsonProperty("totalUsers")] public int TotalUsers;

    [JsonProperty("uptime")] public float Uptime;

    [JsonProperty("version")] public string Version;
    
    [JsonConstructor]
    public StatsResponse(int activeUsers, int totalUsers, float uptime, string version)
    {
        ActiveUsers = activeUsers;
        TotalUsers = totalUsers;
        Uptime = uptime;
        Version = version;
    }
}