using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows all recent conversations.
/// </summary>
public class ListAllConversations : ICommand
{
    public string[] Aliases { get; } = ["lr", "lrc", "recent", "listrecent", "listall", "lall"];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.All);
    }
}