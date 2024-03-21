using System.Net;
using ApertureMessenger.AlmsConnection.Helpers;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Requests;
using Newtonsoft.Json;

namespace ApertureMessenger.AlmsConnection.Repositories;

public static class MessageRepository
{
    public static bool SendMessage(int conversationId, string content)
    {
        var request = new SendMessageRequest(conversationId, content);

        var response = Connector.Post(
            "send-message",
            request.getRequestJson()
        );

        return response.StatusCode == HttpStatusCode.OK;
    }

    public static Message[] GetMessages(int conversationId)
    {
        var response = Connector.Get(
            "get-messages/" + conversationId
        );

        var contentString = ResponseParser.GetResponseContent(response);
        
        var messages = JsonConvert.DeserializeObject<Message[]>(contentString);
        if (messages == null)
        {
            throw new JsonException("Message JSON was empty");
        }
        
        return messages;
    }
}