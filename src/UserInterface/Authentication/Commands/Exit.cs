using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];

    public void Invoke()
    {
        ConsoleWriter.Clear();
        Environment.Exit(0);
    }
}