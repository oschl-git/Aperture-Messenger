using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows the user their recent group conversations.
/// </summary>
public class ListGroupConversations : IActionCommand
{
    public string[] Aliases { get; } = ["listgroup", "listgrp", "lg", "lgc", "lgm"];
    public string Description => "Lists all recent group conversations.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.Group);
    }
}