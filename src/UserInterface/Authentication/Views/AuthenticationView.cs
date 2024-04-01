using ApertureMessenger.UserInterface.Authentication.Commands;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

public class AuthenticationView : IView
{
    private static readonly ICommand[] Commands =
    [
        new Login(),
        new Register(),
        new Exit()
    ];

    public void Process()
    {
        Shared.View = this;
        Shared.CommandResponse = new CommandResponse("Use the :login or :register commands to authenticate.",
            CommandResponse.ResponseType.Info);

        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, Commands);
            
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.CommandResponse = new CommandResponse(
                        "Commands must start with a colon (:).",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.CommandResponse = new CommandResponse(
                        $"{userInput} is not a valid authentication command.",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.Success:
                default:
                    return;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("ALMS AUTHENTICATION");
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "Hello and, again, welcome to Aperture Messenger.", ConsoleColor.Cyan
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(
            "ALMS authentication is required to access ASCAMP services. " +
            "Please, log in or register a new employee account.");

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