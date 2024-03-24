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

    public static LoginResult Login(LoginRequest request)
    {
        var response = Connector.Post(
            "login",
            request.getRequestJson(),
            true
        );
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var contentString = ResponseParser.GetResponseContent(response);
                
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

            case HttpStatusCode.BadRequest:
                throw new BadRequestSent();

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();

            case HttpStatusCode.Unauthorized:
                var errorContent = ResponseParser.GetErrorResponse(response);
                switch (errorContent.Message)
                {
                    case "USER DOES NOT EXIST":
                        return LoginResult.UserDoesNotExist;

                    case "INCORRECT PASSWORD":
                        return LoginResult.IncorrectPassword;
                }

                break;
        }

        throw new UnhandledAuthenticationError();
    }
}