using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];

    public void Invoke(string[] args)
    {
        if (Shared.View is MessagingView)
        {
            ConsoleWriter.Clear();
            Environment.Exit(0);
        }

        Shared.View = new MessagingView();
    }
}