using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Requests;

namespace ApertureMessenger.AlmsConnection.Authentication;

/// <summary>
/// Handles registering new users.
/// </summary>
public static class EmployeeCreator
{
    public static List<string>? LastRegisterAttemptIssues;

    public enum RegisterResult
    {
        Success,
        UsernameTaken,
        RequirementsNotSatisfied
    }

    /// <summary>
    /// Sends a registration request to ALMS.
    /// </summary>
    /// <param name="request">The registration request to send</param>
    /// <returns>Registration attempt result</returns>
    public static RegisterResult Register(RegisterRequest request)
    {
        LastRegisterAttemptIssues = null;

        var response = Connector.Post(
            "register",
            request.GetRequestJson(),
            true
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();

            case HttpStatusCode.BadRequest:
                var errorContent = ResponseParser.GetErrorResponse(response);
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