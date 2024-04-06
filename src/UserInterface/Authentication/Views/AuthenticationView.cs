using System.Text;
using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Queries;
using ApertureMessenger.Logic;
using ApertureMessenger.UserInterface.Authentication.Commands;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

/// <summary>
/// A view/UI handler for displaying the base authentication CLI.
/// </summary>
public class AuthenticationView : IView
{
    private static readonly ICommand[] Commands =
    [
        new Login(),
        new Register(),
        new Exit()
    ];

    private readonly VersionConflictResult? _conflictResult = GetConflictResult();

    private static VersionConflictResult GetConflictResult()
    {
        var targetVersion = Settings.TargetAlmsVersion;
        var actualVersion = Status.GetAlmsStatus().Stats.Version;

        return ConnectionTester.CompareMessengerAndAlmsVersions(targetVersion, actualVersion);
    }

    public void Process()
    {
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

        ComponentWriter.WriteHeader("ALMS AUTHENTICATION");
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "Hello and, again, welcome to Aperture Messenger.", ConsoleColor.Cyan
        );

        PrintVersionConflict();

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(
            "ALMS authentication is required to access ASCAMP services. " +
            "Please, log in or register a new employee account.");

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("Available authentication commands:", ConsoleColor.Yellow);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":login");
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":register");
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":exit");

        ComponentWriter.WriteUserInput();
    }

    private void PrintVersionConflict()
    {
        if (_conflictResult == null) return;
        if (_conflictResult.Result == AlmsVersionComparer.Result.Same) return;

        var message = new StringBuilder();
        message.Append("COMPATIBILITY WARNING: The version of Aperture Messenger you're using is made to be " +
                       $"compatible with ALMS {_conflictResult.TargetVersion}, but you're connected to " +
                       $"{_conflictResult.ActualVersion}. You may experience compatibility issues. ");
        
        switch (_conflictResult.Result)
        {
            case AlmsVersionComparer.Result.Older:
                message.Append("Check for updates online to obtain a new Aperture Messenger version.");
                break;
            case AlmsVersionComparer.Result.Newer:
                message.Append("Consider downgrading or ask the ALMS administrators to update their software.");
                break;
        }

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(message.ToString(), ConsoleColor.Red);
        ConsoleWriter.WriteLine();
    }
}