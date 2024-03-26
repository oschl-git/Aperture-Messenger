using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Repositories;

public static class EmployeeRepository
{
    public static bool IsUsernameTaken(string username)
    {
        var response = Connector.Get(
            "is-username-taken/" + username
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                bool usernameTaken;
                try
                {
                    usernameTaken = JsonConvert.DeserializeObject<bool>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing direct conversation JSON");
                }

                return usernameTaken;
        }

        throw new UnhandledResponseError();
    }
}