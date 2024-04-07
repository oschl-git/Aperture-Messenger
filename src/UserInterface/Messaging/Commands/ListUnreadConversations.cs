using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows the user conversations with messages they haven't
/// read yet.
/// </summary>
public class ListUnreadConversations : IActionCommand
{
    public string[] Aliases { get; } = ["listunread", "listu", "lu", "luc", "lum"];
    public string Description => "Lists conversations with unread messages.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.Unread);
    }
}