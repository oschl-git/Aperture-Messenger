using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
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
        var authenticationDetails = new Dictionary<string, string>
        {
            { "username", username },
            { "password", password }
        };

        var response = Connector.Post(
            "login",
            JsonConvert.SerializeObject(authenticationDetails),
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
                    throw new JsonException("Failed parsing JSON error response");
                }

                if (errorContent == null)
                {
                    throw new JsonException("JSON response was empty");
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
                var content = JsonConvert.DeserializeObject<LoginResponse>(contentString);

                if (content == null)
                {
                    throw new JsonException("Failed parsing ALMS login response");
                }

                Session.GetInstance().SetParameters(content.Token, content.Employee);
                return LoginResult.Success;
        }

        throw new UnhandledLoginError();
    }
}