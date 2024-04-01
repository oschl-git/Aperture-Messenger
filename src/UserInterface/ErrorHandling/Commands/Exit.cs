using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.ErrorHandling.Commands;

public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];
    public void Invoke(string[] args)
    {
        ConsoleWriter.Clear();
        Environment.Exit(0);
    }
}