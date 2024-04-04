using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Responses;

[Serializable]
public class StatusResponse
{
    [JsonProperty("status")] public string Status;

    [JsonProperty("stats")] public StatsResponse Stats;

    [JsonConstructor]
    public StatusResponse(string status, StatsResponse stats)
    {
        Status = status;
        Stats = stats;
    }
}