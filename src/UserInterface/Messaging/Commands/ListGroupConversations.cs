using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class ListGroupConversations : ICommand
{
    public string[] Aliases { get; } = ["lg", "lgc", "lgm", "listgroup", "listgrp"];

    public void Invoke(string[] args)
    {
        Shared.View = new ConversationListView(true);
    }
}