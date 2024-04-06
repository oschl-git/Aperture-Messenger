using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class Logout : IActionCommand
{
    public string[] Aliases { get; } = ["logout"];
    public string Description => "Logs out the currently logged in user.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Session.ClearSession();
        Shared.Feedback = new CommandFeedback("Successfully logged out.", CommandFeedback.FeedbackType.Success);
        Shared.View = new AuthenticationView();
    }
}