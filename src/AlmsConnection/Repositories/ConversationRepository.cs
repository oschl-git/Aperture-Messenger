using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Requests;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Repositories;

/// <summary>
/// Handles access to ALMS conversations.
/// </summary>
public static class ConversationRepository
{
    public static void CreateGroupConversation(CreateGroupConversationRequest request)
    {
        var response = Connector.Post(
            "create-group-conversation",
            request.GetRequestJson()
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

    public static void AddEmployeeToGroup(AddEmployeeToGroupRequest request)
    {
        var response = Connector.Post(
            "add-employee-to-group",
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
                    case "CONVERSATION NOT GROUP":
                        throw new ConversationNotGroup();
                    
                    case "EMPLOYEE ALREADY IN CONVERSATION":
                        throw new EmployeeAlreadyInConversation();
                }

                break;
            
            case HttpStatusCode.NotFound:
                var notFoundResponse = ResponseParser.GetErrorResponse(response);
                switch (notFoundResponse.Message)
                {
                    case "CONVERSATION NOT FOUND":
                        throw new ConversationNotFound();
                    
                    case "EMPLOYEE NOT FOUND":
                        throw new EmployeeDoesNotExist();
                }

                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }

    public static Conversation GetDirectConversation(string username)
    {
        var response = Connector.Get(
            "get-direct-conversation/" + username
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var contentString = ResponseParser.GetResponseContent(response);

                Conversation? conversation;
                try
                {
                    conversation = JsonConvert.DeserializeObject<Conversation>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing direct conversation JSON");
                }

                if (conversation == null) throw new JsonException("Direct conversation JSON was empty");

                return conversation;

            case HttpStatusCode.NotFound:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "EMPLOYEE DOES NOT EXIST":
                        throw new EmployeeDoesNotExist();
                }

                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }

    public static Conversation[] GetAllConversations()
    {
        var response = Connector.Get(
            "get-all-conversations"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Conversation[]? conversations;
                try
                {
                    conversations = JsonConvert.DeserializeObject<Conversation[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing conversations JSON");
                }

                if (conversations == null) throw new JsonException("Conversations JSON was empty");

                return conversations;
        }

        throw new UnhandledResponseError();
    }

    public static Conversation[] GetDirectConversations()
    {
        var response = Connector.Get(
            "get-direct-conversations"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Conversation[]? conversations;
                try
                {
                    conversations = JsonConvert.DeserializeObject<Conversation[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing direct conversations JSON");
                }

                if (conversations == null) throw new JsonException("Direct conversations JSON was empty");

                return conversations;
        }

        throw new UnhandledResponseError();
    }

    public static Conversation[] GetGroupConversations()
    {
        var response = Connector.Get(
            "get-group-conversations"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Conversation[]? conversations;
                try
                {
                    conversations = JsonConvert.DeserializeObject<Conversation[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing group conversations JSON");
                }

                if (conversations == null) throw new JsonException("Group conversations JSON was empty");

                return conversations;
        }

        throw new UnhandledResponseError();
    }
    
    public static Conversation[] GetConversationsWithUnreadMessages()
    {
        var response = Connector.Get(
            "get-unread-conversations"
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Conversation[]? conversations;
                try
                {
                    conversations = JsonConvert.DeserializeObject<Conversation[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing unread conversations JSON");
                }

                if (conversations == null) throw new JsonException("Unread conversations JSON was empty");

                return conversations;
        }

        throw new UnhandledResponseError();
    }
    
    public static Conversation GetConversationById(int id)
    {
        var response = Connector.Get(
            "get-conversation-by-id/" + id
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                var contentString = ResponseParser.GetResponseContent(response);
                Conversation? conversation;
                try
                {
                    conversation = JsonConvert.DeserializeObject<Conversation>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing conversation JSON");
                }

                if (conversation == null) throw new JsonException("Conversation JSON was empty");

                return conversation;

            case HttpStatusCode.NotFound:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "CONVERSATION NOT FOUND":
                        throw new ConversationNotFound();
                }

                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }
}