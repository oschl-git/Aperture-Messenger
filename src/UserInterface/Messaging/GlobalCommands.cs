using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Commands;

namespace ApertureMessenger.UserInterface.Messaging;

public static class GlobalCommands
{
    public static readonly ICommand[] Commands =
    [
        new DirectMessage(),
        new CreateGroupConversation(),
        new ConversationById(),
        new ListGroupConversations(),
        new ListDirectConversations(),
        new Exit()
    ];
}