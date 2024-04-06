using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.ErrorHandling.Commands;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.ErrorHandling.Views;

/// <summary>
/// A view/UI handler for displaying the error view CLI.
/// </summary>
public class ErrorView : IView
{
    private static readonly IActionCommand[] Commands =
    [
        new Retry(),
        new Exit()
    ];

    private readonly Exception _exception;

    public ErrorView(Exception exception)
    {
        _exception = exception;
    }

    public void Process()
    {
        Shared.Feedback = new CommandFeedback("Use :retry to restart the application or :exit to exit.",
            CommandFeedback.FeedbackType.Info);
        Shared.UserInput = "";

        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, Commands);

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

        ComponentWriter.WriteHeader("FATAL RUNTIME ERROR", ConsoleColor.DarkRed);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            $"\u26a0 {GetMessage()}", ConsoleColor.Red
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "If this is unexpected behaviour, please create an issue with the details below at " +
            "github.com/oschl-git/aperture-messenger. Your help is greatly appreciated!"
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteLine("DETAILS:", ConsoleColor.Yellow);
        ConsoleWriter.WriteWithWordWrap(_exception.ToString(), ConsoleColor.Red);

        ComponentWriter.WriteUserInput();
    }

    private string GetMessage()
    {
        return _exception switch
        {
            FailedContactingAlms => "Failed contacting ALMS. Check your internet connection.",
            TooManyRequests => "You've sent too many ALMS requests. Try again in a few minutes.",
            InternalAlmsError => "An internal ALMS error occurred.",
            TokenInvalid => "Your ALMS token is invalid. Have you logged in somewhere else? " +
                            "Restart the application with :retry and login again to continue on this computer.",
            TokenExpired => "Your ALMS token has expired. Please, restart the application with :retry and login again.",
            _ => "An unexpected error has occurred during the runtime of Aperture Messenger."
        };
    }
}