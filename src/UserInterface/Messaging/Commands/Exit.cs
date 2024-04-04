using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// An command that handles exiting the application or returning to the default messaging view depending on the context.
/// </summary>
public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];

    public void Invoke(string[] args)
    {
        if (Shared.View is MessagingView)
        {
            System.Console.ResetColor();
            ConsoleWriter.Clear();
            Environment.Exit(0);
        }

        Shared.View = new MessagingView();
    }
}