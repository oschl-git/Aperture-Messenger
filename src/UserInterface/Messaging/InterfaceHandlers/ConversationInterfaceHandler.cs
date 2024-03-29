using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.InterfaceHandlers;

public class ConversationInterfaceHandler : IInterfaceHandler
{
    private static readonly ICommand[] Commands =
    [
    ];

    private readonly Conversation _conversation;
    private readonly List<Message> _messages;

    public ConversationInterfaceHandler(Conversation conversation)
    {
        _conversation = conversation;
        _messages = MessageRepository.GetMessages(_conversation.Id).ToList();
    }

    public void Process()
    {
        SharedData.InterfaceHandler = this;
        SharedData.CommandResponse = new CommandResponse(
            "Input is sent as a message unless you prefix it with a colon (:).",
            CommandResponse.ResponseType.Info
        );

        while (true)
        {
            DrawUserInterface();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, Commands);

            if (commandResult == CommandProcessor.Result.NotACommand)
            {
                MessageRepository.SendMessage(new SendMessageRequest(_conversation.Id, userInput));
                GetNewMessages();
                continue;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader(GetHeaderContent());

        foreach (var message in _messages)
        {
            var employeeString = $"{message.Employee.Username}: ";
            var employeeColor = message.Employee.Username == Session.GetInstance().Employee?.Username
                ? ConsoleColor.Magenta
                : ConsoleColor.Cyan;

            ConsoleWriter.Write(employeeString, employeeColor);
            ConsoleWriter.WriteWithWordWrap(
                message.Content, ConsoleColors.DefaultForegroundColor, employeeString.Length
            );
            ConsoleWriter.WriteLine();
        }

        ComponentWriter.WriteUserInput($"{Session.GetInstance().Employee?.Username}>");
    }

    private string GetHeaderContent()
    {
        if (_conversation.Participants == null) return $"A MYSTERIOUS CONVERSATION (ID: {_conversation.Id}";

        if (_conversation.IsGroup)
        {
            return $"GROUP CONVERSATION \"{_conversation.Name}\" ({_conversation.Participants.Count} members)";
        }

        string? otherEmployee = null;
        foreach (var employee in _conversation.Participants)
        {
            if (employee.Name == Session.GetInstance().Employee?.Name) continue;
            otherEmployee = employee.Name;
            break;
        }

        return $"DIRECT CONVERSATION WITH \"{otherEmployee}\"";
    }

    public void GetNewMessages()
    {
        var unreadMessages = MessageRepository.GetUnreadMessages(_conversation.Id);
        foreach (var message in unreadMessages)
        {
            _messages.Add(message);
        }
    }
}