using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Help;

/// <summary>
/// A view/UI handler for displaying the help information.
/// </summary>
public class HelpView : IView
{
    private readonly IActionCommand[] _currentContext;
    private readonly IActionCommand? _specifiedCommand;

    public HelpView(IActionCommand[] currentContext, IActionCommand? specifiedCommand = null)
    {
        _currentContext = currentContext;
        _specifiedCommand = specifiedCommand;
    }

    public void Process()
    {
        while (true)
        {
            DrawUserInterface();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, _currentContext);

            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.Feedback = new CommandFeedback(
                        "Commands must start with a colon (:).",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Feedback = new CommandFeedback(
                        "The provided input is not a valid command in this context.",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.Success:
                default:
                    return;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader(GetTitle(), ConsoleColor.Yellow);

        if (_specifiedCommand == null)
        {
            foreach (var command in _currentContext)
            {
                PrintHelpForCommand(command);
                ConsoleWriter.WriteLine();
            }

            PrintHelpForCommand(new HelpCommand());
        }
        else
        {
            PrintHelpForCommand(_specifiedCommand);
        }

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }

    private string GetTitle()
    {
        return _specifiedCommand != null
            ? $"\"{_specifiedCommand.Aliases[0].ToUpper()}\" COMMAND HELP"
            : "COMMAND HELP";
    }

    private static void PrintHelpForCommand(ICommand command)
    {
        ConsoleWriter.WriteLine();
        ConsoleWriter.Write(" - ");
        ConsoleWriter.Write($"{command.Aliases[0]}", ConsoleColor.Yellow);
        ConsoleWriter.WriteWithWordWrap(
            $" = {command.Description}", ConsoleColor.Cyan, command.Aliases[0].Length + 3
        );

        if (command.Aliases.Length > 1)
        {
            const string aliasesTitleString = "     aliases: ";

            ConsoleWriter.WriteLine();
            ConsoleWriter.Write(aliasesTitleString, ConsoleColor.Green);
            ConsoleWriter.WriteWithWordWrap(
                string.Join(", ", command.Aliases.Skip(1)), ConsoleColor.Yellow, aliasesTitleString.Length
            );
        }

        if (command.Arguments.Length > 0)
        {
            ConsoleWriter.WriteLine();
            ConsoleWriter.Write("     arguments:", ConsoleColor.Magenta);

            var index = 1;
            foreach (var argument in command.Arguments)
            {
                var indexString = $"     {index.ToString()}. ";
                var argumentNameString = $"{argument.Item1}";

                ConsoleWriter.WriteLine();
                ConsoleWriter.Write(indexString);
                ConsoleWriter.Write(argumentNameString, ConsoleColor.Yellow);
                ConsoleWriter.WriteWithWordWrap(
                    $" - {argument.Item2}",
                    firstLineOffset: indexString.Length + argumentNameString.Length
                );

                index++;
            }
        }
    }
}