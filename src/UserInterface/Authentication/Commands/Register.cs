using ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Register : ICommand
{
    public string[] Aliases { get; } = ["register", "r"];

    public void Invoke(string[] args)
    {
        SharedData.InterfaceHandler = new RegisterInterfaceHandler();
        SharedData.InterfaceHandler.Process();
    }
}