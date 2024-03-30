using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Register : ICommand
{
    public string[] Aliases { get; } = ["register", "r"];

    public void Invoke(string[] args)
    {
        SharedData.View = new RegisterView();
        SharedData.View.Process();
    }
}