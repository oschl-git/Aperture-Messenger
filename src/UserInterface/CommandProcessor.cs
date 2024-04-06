using ApertureMessenger.UserInterface.Help;
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

    public static Result InvokeCommand(string userInput, IActionCommand[] commands, bool disableHelp = false)
    {
        // Check if input is a command
        if (!userInput.StartsWith(':')) return Result.NotACommand;

        // Process input
        var processedInput = userInput[1..].Split(' ');
        var args = processedInput.Skip(1).ToArray();

        // Process help commands
        if (!disableHelp)
        {
            var helpCommand = GetHelpCommand(processedInput[0]);
            if (helpCommand != null)
            {
                if (args.Length > 0)
                {
                    var specifiedCommand = GetActionCommand(args[0], commands);
                    helpCommand.Invoke(commands, specifiedCommand);
                }
                else helpCommand.Invoke(commands);

                return Result.Success;
            }
        }
        
        // Try to get the appropriate command
        var command = GetActionCommand(processedInput[0], commands);
        if (command == null) return Result.InvalidCommand;

        // Invoke the command and return success
        command.Invoke(args);
        return Result.Success;
    }

    private static IActionCommand? GetActionCommand(string submittedCommand, IActionCommand[] commands)
    {
        foreach (var command in commands)
        {
            if (Array.Exists(command.Aliases, alias => alias == submittedCommand.ToLower()))
            {
                return command;
            }
        }

        return null;
    }

    private static HelpCommand? GetHelpCommand(string submittedCommand)
    {
        var helpCommand = new HelpCommand();

        if (Array.Exists(helpCommand.Aliases, alias => alias == submittedCommand.ToLower()))
        {
            return helpCommand;
        }

        return null;
    }
}