using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Queries;

public static class Status
{
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