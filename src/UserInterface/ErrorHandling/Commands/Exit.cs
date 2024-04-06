using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.ErrorHandling.Commands;

/// <summary>
/// An error handling command that handles exiting the application.
/// </summary>
public class Exit : IActionCommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];
    public string Description => "Exits the application";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        System.Console.ResetColor();
        ConsoleWriter.Clear();
        Environment.Exit(0);
    }
}