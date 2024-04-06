using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

/// <summary>
/// An authentication command that handles exiting the application or returning to the default view depending on the
/// context.
/// </summary>
public class Exit : IActionCommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];
    public string Description => "Returns back to the previous context or exits the application.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        if (Shared.View is AuthenticationView)
        {
            System.Console.ResetColor();
            ConsoleWriter.Clear();
            Environment.Exit(0);
        }

        Shared.Feedback = new CommandFeedback(
            "Use the :login or :register commands to authenticate.",
            CommandFeedback.FeedbackType.Info
        );

        Shared.View = new AuthenticationView();
    }
}