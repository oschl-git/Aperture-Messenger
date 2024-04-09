using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Requests;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Repositories;

/// <summary>
/// Handles access to ALMS employees.
/// </summary>
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

    public static Employee[] GetAllEmployees()
    {
        var response = Connector.Get(
            "get-all-employees"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Employee[]? employees;
                try
                {
                    employees = JsonConvert.DeserializeObject<Employee[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing employees JSON");
                }

                if (employees == null) throw new JsonException("Employees JSON was empty");

                return employees;
        }

        throw new UnhandledResponseError();
    }

    public static Employee[] GetOnlineEmployees()
    {
        var response = Connector.Get(
            "get-active-employees"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Employee[]? employees;
                try
                {
                    employees = JsonConvert.DeserializeObject<Employee[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing online employees JSON");
                }

                if (employees == null) throw new JsonException("Online employees JSON was empty");

                return employees;
        }

        throw new UnhandledResponseError();
    }

    public static void ChangeEmployeeColor(SetEmployeeColorRequest request)
    {
        var response = Connector.Post(
            "set-employee-color",
            request.GetRequestJson()
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return;

            case HttpStatusCode.BadRequest:
                var badRequestResponse = ResponseParser.GetErrorResponse(response);
                switch (badRequestResponse.Message)
                {
                    case "INVALID COLOR":
                        throw new InvalidColor();
                }
                
                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }
}