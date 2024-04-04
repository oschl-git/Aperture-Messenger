using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows the user their recent direct conversations.
/// </summary>
public class ListDirectConversations : ICommand
{
    public string[] Aliases { get; } = ["ld", "ldc", "ldm", "listdirect"];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.Direct);
    }
}