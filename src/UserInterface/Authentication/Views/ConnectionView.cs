using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

/// <summary>
/// A view/UI handler that is displayed while an ALMS connection is being established.
/// </summary>
public class ConnectionView : IView
{
    public void Process()
    {
        DrawUserInterface();

        if (!ConnectionTester.TestConnection()) throw new FailedContactingAlms();

        Shared.Feedback = new CommandFeedback("Use the :login or :register commands to authenticate.",
            CommandFeedback.FeedbackType.Info);

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