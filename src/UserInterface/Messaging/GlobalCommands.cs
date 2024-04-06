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
        new CreateGroupConversation(),
        new ConversationById(),
        new ListAllConversations(),
        new ListGroupConversations(),
        new ListDirectConversations(),
        new Logout(),
        new Exit()
    ];
}