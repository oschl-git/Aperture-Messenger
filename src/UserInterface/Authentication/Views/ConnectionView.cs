using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;

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
        
        Shared.View = AuthenticationView.GetInstance();
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("CONNECTING TO ALMS...", ConsoleColor.Gray);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("\ud83d\udec8 Establishing a connection with ALMS. Please be patient.");
    }
}