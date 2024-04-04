using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

/// <summary>
/// A base messaging view/UI handler.
/// </summary>
public class MessagingView : IView
{
    public void Process()
    {
        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.Response = new CommandResponse(
                        "Commands must start with a colon (:).",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Response = new CommandResponse(
                        $"{userInput} is not a valid command in this context.",
                        CommandResponse.ResponseType.Error
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

        ComponentWriter.WriteHeader("APERTURE MESSAGING INTERFACE");
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap("Successfully authenticated and connected to ALMS.", ConsoleColor.Green);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            $"Welcome, employee {Session.Employee?.Id} ({Session.Employee?.Name} {Session.Employee?.Surname}).",
            ConsoleColor.Cyan);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(
            "You are now able to message other Aperture Science and Laboratories personnel. If you're lost, use the :help command to get a list of available actions.");

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }
}