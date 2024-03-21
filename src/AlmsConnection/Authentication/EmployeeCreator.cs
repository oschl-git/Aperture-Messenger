using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Authentication;

public static class EmployeeCreator
{
    public static List<string>? LastRegisterAttemptIssues;

    public enum RegisterResult
    {
        Success,
        UsernameTaken,
        RequirementsNotSatisfied
    }

    public static RegisterResult Register(string username, string name, string surname, string password)
    {
        var registerRequest = new RegisterRequest(username, name, surname, password);

        var response = Connector.Post(
            "register",
            registerRequest.getRequestJson(),
            true
        );

        var contentString = ResponseParser.GetResponseContent(response);
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();

            case HttpStatusCode.BadRequest:
                ErrorResponse? errorContent;
                try
                {
                    errorContent = JsonConvert.DeserializeObject<ErrorResponse>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing JSON register error response");
                }

                if (errorContent == null)
                {
                    throw new JsonException("JSON register error response was empty");
                }

                switch (errorContent.Message)
                {
                    case "REQUIREMENTS NOT SATISFIED":
                        LastRegisterAttemptIssues = errorContent.Errors;
                        return RegisterResult.RequirementsNotSatisfied;

                    case "USERNAME TAKEN":
                        return RegisterResult.UsernameTaken;
                }

                break;

            case HttpStatusCode.OK:
                return RegisterResult.Success;
        }

        throw new UnhandledAuthenticationError();
    }
}