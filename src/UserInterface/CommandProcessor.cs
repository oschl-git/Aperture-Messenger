using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface;

/// <summary>
/// Handles invoking commands.
/// </summary>
public static class CommandProcessor
{
    public enum Result
    {
        Success,
        NotACommand,
        InvalidCommand
    }

    public static Result InvokeCommand(string userInput, ICommand[] commands)
    {
        if (!userInput.StartsWith(':')) return Result.NotACommand;

        var processedInput = userInput[1..].Split(' ');

        var command = GetCommand(processedInput[0], commands);
        if (command == null) return Result.InvalidCommand;

        command.Invoke(processedInput.Skip(1).ToArray());

        return Result.Success;
    }

    private static ICommand? GetCommand(string submittedCommand, ICommand[] commands)
    {
        foreach (var command in commands)
            if (Array.Exists(command.Aliases, alias => alias == submittedCommand.ToLower()))
                return command;

        return null;
    }
}