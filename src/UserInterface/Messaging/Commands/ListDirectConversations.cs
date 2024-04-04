using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class ListDirectConversations : ICommand
{
    public string[] Aliases { get; } = ["ld", "ldc", "ldm", "listdirect"];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(false);
    }
}