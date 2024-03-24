using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Requests;

namespace ApertureMessenger.AlmsConnection.Repositories;

public static class ConversationRepository
{   
    public static void CreateGroupConversation(CreateGroupConversationRequest request)
    {
        var response = Connector.Post(
            "create-group-conversation",
            request.getRequestJson()
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return;
            
            case HttpStatusCode.BadRequest:
                var errorResponse = ResponseParser.GetErrorResponse(response);
                switch (errorResponse.Message)
                {
                    case "EMPLOYEES DO NOT EXIST":
                        throw new EmployeesDoNotExist(errorResponse.Usernames?.ToArray() ?? Array.Empty<string>());
                }

                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }

}