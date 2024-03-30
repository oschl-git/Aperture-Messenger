using ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Commands;

public class Exit : ICommand
{
    public string[] Aliases { get; } = ["exit", "quit", "e", "q"];

    public void Invoke(string[] args)
    {
        if (SharedData.InterfaceHandler == AuthenticationInterfaceHandler.GetInstance())
        {
            ConsoleWriter.Clear();
            Environment.Exit(0);
        }

        SharedData.CommandResponse = new CommandResponse(
            "Use the :login or :register commands to authenticate.",
            CommandResponse.ResponseType.Info
        );
        SharedData.InterfaceHandler = AuthenticationInterfaceHandler.GetInstance();
    }
}