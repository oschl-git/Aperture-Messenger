using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Queries;
using ApertureMessenger.AlmsConnection.Responses;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Fun;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

/// <summary>
/// A base messaging view/UI handler.
/// </summary>
public class MessagingView : IView
{
    private StatusResponse _status = Status.GetAlmsStatus();
    private string _apertureQuote = ApertureQuotes.GetRandomQuote();

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

        ComponentWriter.WriteHeader("MESSAGING INTERFACE", ConsoleColor.DarkCyan);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap("Successfully authenticated and connected to ALMS.", ConsoleColor.Green);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            $"Welcome, employee {Session.Employee?.Id}. There are currently {_status.Stats.ActiveUsers} " +
            $"employees online out of {_status.Stats.TotalUsers} total registered accounts.",
            ConsoleColor.Cyan);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(_apertureQuote);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "Use the :help command to see a list of available actions.", ConsoleColor.Yellow
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }
}