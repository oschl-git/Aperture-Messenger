using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.AlmsConnection.Responses;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Authentication;

public static class Authenticator
{
    public enum LoginResult
    {
        Success,
        UserDoesNotExist,
        IncorrectPassword
    }

    public static LoginResult Login(string username, string password)
    {
        var loginRequest = new LoginRequest(username, password);

        var response = Connector.Post(
            "login",
            loginRequest.getRequestJson(),
            true
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                throw new BadRequestSent();

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();

            case HttpStatusCode.Unauthorized:
                ErrorResponse? errorContent;
                try
                {
                    errorContent = JsonConvert.DeserializeObject<ErrorResponse>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing JSON login error response");
                }

                if (errorContent == null)
                {
                    throw new JsonException("JSON login error response was empty");
                }

                switch (errorContent.Message)
                {
                    case "USER DOES NOT EXIST":
                        return LoginResult.UserDoesNotExist;

                    case "INCORRECT PASSWORD":
                        return LoginResult.IncorrectPassword;
                }

                break;

            case HttpStatusCode.OK:
                LoginResponse? content;
                try
                {
                    content = JsonConvert.DeserializeObject<LoginResponse>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing JSON login response");
                }
                
                if (content == null)
                {
                    throw new JsonException("JSON login response was empty");
                }

                Session.GetInstance().SetParameters(content.Token, content.Employee);
                return LoginResult.Success;
        }

        throw new UnhandledAuthenticationError();
    }
}