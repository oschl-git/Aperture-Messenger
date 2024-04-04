using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

public class ConnectionView : IView
{
    public void Process()
    {
        DrawUserInterface();
        
        if (!ConnectionTester.TestConnection())
        {
            throw new FailedContactingAlms();
        }
        
        
        Shared.CommandResponse = new CommandResponse("Use the :login or :register commands to authenticate.",
            CommandResponse.ResponseType.Info);
        Shared.View = new AuthenticationView();
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("CONNECTING TO ALMS...", ConsoleColor.Gray);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("\ud83d\udec8 Establishing a connection with ALMS. Please be patient.");
    }
}