using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

/// <summary>
/// An authentication command that handles switching to the login view.
/// </summary>
public class Login : IActionCommand
{
    public string[] Aliases { get; } = ["login", "l"];
    public string Description => "Initiates login.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new LoginView();
    }
}