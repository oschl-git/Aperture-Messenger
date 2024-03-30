using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.InterfaceHandlers;

public class MessagingInterfaceHandler : IInterfaceHandler
{
    private static readonly MessagingInterfaceHandler Instance = new();

    private MessagingInterfaceHandler()
    {
    }

    public static MessagingInterfaceHandler GetInstance()
    {
        return Instance;
    }

    public void Process()
    {
        SharedData.InterfaceHandler = this;

        if (SharedData.CommandResponse?.Type == CommandResponse.ResponseType.Info)
        {
            SharedData.CommandResponse = new CommandResponse(
                "Type :help to get information about available actions.",
                CommandResponse.ResponseType.Info
            );
        }
        
        while (true)
        {
            DrawUserInterface();
            
            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    SharedData.CommandResponse = new CommandResponse(
                        "Commands must start with a colon (:).",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    SharedData.CommandResponse = new CommandResponse(
                        $"{userInput} is not a valid command.",
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

        ComponentWriter.WriteHeader("APERTURE MESSAGING INTERFACE");
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap("Successfully authenticated and connected to ALMS.", ConsoleColor.Green);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();
        
        ConsoleWriter.WriteWithWordWrap($"Welcome, employee {Session.GetInstance().Employee?.Id} ({Session.GetInstance().Employee?.Name} {Session.GetInstance().Employee?.Surname}).", ConsoleColor.Cyan);
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap(
            "You are now able to message other Aperture Science and Laboratories personnel. If you're lost, use the :help command to get a list of available actions.");

        ComponentWriter.WriteUserInput($"{Session.GetInstance().Employee?.Username}>");
    }
}