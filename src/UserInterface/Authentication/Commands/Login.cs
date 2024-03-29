using ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Login : ICommand
{
    public string[] Aliases { get; } = ["login", "l"];

    public void Invoke(string[] args)
    {
        SharedData.InterfaceHandler = new LoginInterfaceHandler();
        SharedData.InterfaceHandler.Process();
    }
}