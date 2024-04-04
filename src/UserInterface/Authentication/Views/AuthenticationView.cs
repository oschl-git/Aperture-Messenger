using System.Configuration;
using ApertureMessenger.AlmsConnection;
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

    private VersionConflictResult? _conflictResult;

    public AuthenticationView()
    {
        _conflictResult = GetConflictResult();
    }

    private VersionConflictResult? GetConflictResult()
    {
        var targetVersion = ConfigurationManager.AppSettings.Get("TargetAlmsVersion");
        if (targetVersion == null) return null;

        var actualVersion = ConnectionTester.GetAlmsStatus().Stats.Version;

        return new VersionConflictResult(targetVersion, actualVersion);
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
                    Shared.Response = new CommandResponse(
                        "Commands must start with a colon (:).",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Response = new CommandResponse(
                        "The provided input is not a valid command in this context.",
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
        
        switch (_conflictResult.Result)
        {
            case AlmsVersionComparer.Result.Older:
                ConsoleWriter.WriteLine();
                ConsoleWriter.WriteLine();
                ConsoleWriter.WriteWithWordWrap(
                    "WARNING: Your version of Aperture Messenger is made for an older " +
                    "version of ALMS than you're connected to. Check if an update has been released.",
                    ConsoleColor.Red
                );
                ConsoleWriter.WriteLine();
                break;
            case AlmsVersionComparer.Result.Newer:
                ConsoleWriter.WriteLine();
                ConsoleWriter.WriteLine();
                ConsoleWriter.WriteWithWordWrap(
                    "WARNING: Your version of Aperture Messenger is made for a newer " +
                    "version of ALMS than you're connected to. Consider downgrading or switching to a different ALMS.",
                    ConsoleColor.Red
                );
                ConsoleWriter.WriteLine();
                break;
            case AlmsVersionComparer.Result.Same:
            default:
                return;
        }
    }
}