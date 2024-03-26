using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;

public class LoginInterfaceHandler : IInterfaceHandler
{
    public void Process()
    {
        SharedData.InterfaceHandler = this;
        SharedData.CommandResponse = new CommandResponse("Logging in.",
            CommandResponse.ResponseType.Info);
        
        DrawUserInterface();
        while (true)
        {
            
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("ALMS EMPLOYEE LOGIN", ConsoleColor.Cyan);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap("Logging in to an existing account.", ConsoleColor.DarkCyan);

        ComponentWriter.WriteUserInput();
    }
}