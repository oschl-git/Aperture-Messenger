using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.ErrorHandling.Commands;

/// <summary>
/// An error handling command that handles exiting the application.
/// </summary>
public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];

    public void Invoke(string[] args)
    {
        System.Console.ResetColor();
        ConsoleWriter.Clear();
        Environment.Exit(0);
    }
}