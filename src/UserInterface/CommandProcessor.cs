using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface;

public static class CommandProcessor
{
    public static ICommand? GetCommand(string userInput, ICommand[] commands)
    {
        if (!userInput.StartsWith(':')) return null;

        var submittedCommand = userInput[1..];
        foreach (var command in commands)
        {
            if (Array.Exists(command.Aliases, alias => alias == submittedCommand.ToLower()))
            {
                return command;
            }
        }

        return null;
    }
}