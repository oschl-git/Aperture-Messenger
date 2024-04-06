using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows all recent conversations.
/// </summary>
public class ListAllConversations : IActionCommand
{
    public string[] Aliases { get; } = ["listrecent", "lrecent", "recent", "listall", "lr", "lrc", "lall"];
    public string Description => "Lists all recent conversations.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.All);
    }
}