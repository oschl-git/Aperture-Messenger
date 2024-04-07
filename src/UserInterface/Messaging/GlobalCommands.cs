using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Commands;

namespace ApertureMessenger.UserInterface.Messaging;

/// <summary>
/// Global messaging commands.
/// </summary>
public static class GlobalCommands
{
    public static readonly IActionCommand[] Commands =
    [
        new DirectMessage(),
        new ConversationById(),
        new ListUnreadConversations(),
        new ListAllConversations(),
        new ListDirectConversations(),
        new ListGroupConversations(),
        new CreateGroupConversation(),
        new Logout(),
        new Exit()
    ];
}