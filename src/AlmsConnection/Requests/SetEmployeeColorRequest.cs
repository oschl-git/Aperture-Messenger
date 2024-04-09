using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Requests;

[Serializable]
public class SetEmployeeColorRequest : IRequest
{
    [JsonProperty("color")] public int ColorNumber;
    
    [JsonConstructor]
    public SetEmployeeColorRequest(int colorNumber)
    {
        ColorNumber = colorNumber;
    }

    public string GetRequestJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}