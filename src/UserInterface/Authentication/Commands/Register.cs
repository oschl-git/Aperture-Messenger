using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

/// <summary>
/// An authentication command that handles switching to the register view.
/// </summary>
public class Register : IActionCommand
{
    public string[] Aliases { get; } = ["register", "r"];
    public string Description => "Initiates registration.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new RegisterView();
    }
}