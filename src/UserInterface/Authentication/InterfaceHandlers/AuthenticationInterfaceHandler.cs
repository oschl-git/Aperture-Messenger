using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Authentication.Commands;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;

public class AuthenticationInterfaceHandler : IInterfaceHandler
{
    private static readonly AuthenticationInterfaceHandler Instance = new();

    private static readonly ICommand[] Commands =
    [
        new Login(),
        new Register(),
        new Exit()
    ];

    private AuthenticationInterfaceHandler()
    {
    }

    public static AuthenticationInterfaceHandler GetInstance()
    {
        return Instance;
    }

    public void Process()
    {
        SharedData.InterfaceHandler = this;
        SharedData.CommandResponse = new CommandResponse("Use the :login or :register commands to authenticate.",
            CommandResponse.ResponseType.Info);

        while (!Session.GetInstance().IsAuthenticated())
        {
            DrawUserInterface();

            var userInput = ConsoleReader.ReadCommandFromUser();
            if (!userInput.StartsWith(':'))
            {
                SharedData.CommandResponse = new CommandResponse(
                    "Commands must start with a colon (:).",
                    CommandResponse.ResponseType.Error
                );
                continue;
            }

            var command = CommandProcessor.GetCommand(userInput, Commands);

            if (command == null)
            {
                SharedData.CommandResponse = new CommandResponse(
                    $"{userInput} is not a valid authentication command.", CommandResponse.ResponseType.Error
                );
                continue;
            }

            command.Invoke();
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("ALMS AUTHENTICATION");
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap("Hello and, again, welcome to Aperture Messenger.", ConsoleColor.DarkCyan);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(
            "ALMS (ASAP) authentication is required to access Aperture Intelligence services. Please, log in or register a new employee account.");

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("Available authentication commands:", ConsoleColor.Yellow);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":login");
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":register");
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(":exit");

        ComponentWriter.WriteUserInput();
    }
}