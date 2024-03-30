using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Login : ICommand
{
    public string[] Aliases { get; } = ["login", "l"];

    public void Invoke(string[] args)
    {
        SharedData.View = new LoginView();
        SharedData.View.Process();
    }
}