using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows the user their recent direct conversations.
/// </summary>
public class ListDirectConversations : IActionCommand
{
    public string[] Aliases { get; } = ["listdirect", "listd", "ld", "ldc", "ldm"];
    public string Description => "Lists all recent direct conversations.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.Direct);
    }
}