using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the conversation list view and shows the user their recent group conversations.
/// </summary>
public class ListGroupConversations : ICommand
{
    public string[] Aliases { get; } = ["lg", "lgc", "lgm", "listgroup", "listgrp"];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(ConversationListView.ConversationType.Group);
    }
}