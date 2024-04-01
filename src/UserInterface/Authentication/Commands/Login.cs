using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Login : ICommand
{
    public string[] Aliases { get; } = ["login", "l"];

    public void Invoke(string[] args)
    {
        Shared.View = new LoginView();
    }
}