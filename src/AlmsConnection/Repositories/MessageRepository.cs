using System.Net;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Requests;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Repositories;

public static class MessageRepository
{
    public static void SendMessage(SendMessageRequest request)
    {
        var response = Connector.Post(
            "send-message",
            request.getRequestJson()
        );

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return;

            case HttpStatusCode.NotFound:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "CONVERSATION NOT FOUND":
                        throw new ConversationNotFound();
                }

                break;

            case HttpStatusCode.BadRequest:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "CONTENT TOO LONG":
                        throw new MessageContentWasTooLong();
                }

                break;

            case HttpStatusCode.InternalServerError:
                throw new InternalAlmsError();
        }

        throw new UnhandledResponseError();
    }

    public static Message[] GetMessages(int conversationId)
    {
        var response = Connector.Get(
            "get-messages/" + conversationId
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Message[]? messages;
                try
                {
                    messages = JsonConvert.DeserializeObject<Message[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing message JSON");
                }

                if (messages == null)
                {
                    throw new JsonException("Message JSON was empty");
                }

                return messages;

            case HttpStatusCode.NotFound:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "CONVERSATION NOT FOUND":
                        throw new ConversationNotFound();
                }

                break;
        }

        throw new UnhandledResponseError();
    }
    
    public static Message[] GetUnreadMessages(int conversationId)
    {
        var response = Connector.Get(
            "get-unread-messages/" + conversationId
        );

        var contentString = ResponseParser.GetResponseContent(response);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                Message[]? messages;
                try
                {
                    messages = JsonConvert.DeserializeObject<Message[]>(contentString);
                }
                catch (Exception)
                {
                    throw new JsonException("Failed parsing unread message JSON");
                }

                if (messages == null)
                {
                    throw new JsonException("Unread message JSON was empty");
                }

                return messages;

            case HttpStatusCode.NotFound:
                switch (ResponseParser.GetErrorResponse(response).Message)
                {
                    case "CONVERSATION NOT FOUND":
                        throw new ConversationNotFound();
                }

                break;
        }

        throw new UnhandledResponseError();
    }
}