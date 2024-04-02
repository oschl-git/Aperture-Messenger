using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class ConversationById : ICommand
{
    public string[] Aliases { get; } = ["conversationid", "cid"];
    public void Invoke(string[] args)
    {
        if (args.Length == 0)
        {
            Shared.CommandResponse = new CommandResponse(
                "Missing argument: You must provide the ID of the conversation.",
                CommandResponse.ResponseType.Error
            );
            return;
        }

        if (args.Length > 1)
        {
            Shared.CommandResponse = new CommandResponse(
                "Too many arguments for command.",
                CommandResponse.ResponseType.Error
            );
            return;
        }
        
        Conversation conversation;
        try
        {
            conversation = ConversationRepository.GetConversationById(int.Parse(args[0]));
        }
        catch (ConversationNotFound)
        {
            Shared.CommandResponse = new CommandResponse(
                "The provided conversation doesn't exist.",
                CommandResponse.ResponseType.Error
            );
            return;
        }
        
        Shared.View = new ConversationView(conversation);
    }
}