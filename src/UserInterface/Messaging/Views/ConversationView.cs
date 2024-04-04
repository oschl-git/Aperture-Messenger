using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

/// <summary>
/// A view/UI handler for displaying a live conversation.
/// </summary>
public class ConversationView : IView
{
    private readonly Conversation _conversation;
    private readonly List<Message> _messages;

    public ConversationView(Conversation conversation)
    {
        _conversation = conversation;

        var messages = MessageRepository.GetMessages(_conversation.Id).ToList();
        messages.Reverse();
        _messages = messages;
    }

    public void Process()
    {
        Shared.Response = new CommandResponse(
            "Input is sent as a message unless you prefix it with a colon (:).",
            CommandResponse.ResponseType.Info
        );

        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);

            if (commandResult == CommandProcessor.Result.NotACommand)
            {
                SendMessage(userInput);
                continue;
            }

            switch (commandResult)
            {
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Response = new CommandResponse(
                        "The provided input is not a valid command in this context.",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.Success:
                default:
                    return;
            }
        }
    }

    private void SendMessage(string content)
    {
        try
        {
            MessageRepository.SendMessage(new SendMessageRequest(_conversation.Id, content));
        }
        catch (MessageContentWasTooLong)
        {
            Shared.Response = new CommandResponse(
                "The message was too long to be sent.",
                CommandResponse.ResponseType.Error
            );
            Shared.UserInput = content;
            return;
        }

        Shared.GetNewMessages();
        Shared.Response = new CommandResponse(
            "Message sent.",
            CommandResponse.ResponseType.Success
        );
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader(GetHeaderContent(), GetHeaderColor());

        foreach (var message in _messages)
        {
            var employeeString = $"{message.Employee.Username} [{message.DateTimeSent.ToLocalTime()}]: ";
            var employeeColor = message.Employee.Username == Session.Employee?.Username
                ? ConsoleColor.Magenta
                : ConsoleColor.Cyan;

            ConsoleWriter.Write(employeeString, employeeColor);
            ConsoleWriter.WriteWithWordWrap(
                message.Content, ConsoleColors.DefaultForegroundColor, employeeString.Length
            );
            ConsoleWriter.WriteLine();
        }

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }

    private string GetHeaderContent()
    {
        if (_conversation.Participants == null) return $"A MYSTERIOUS CONVERSATION (ID: {_conversation.Id})";

        if (_conversation.IsGroup)
            return $"GROUP CONVERSATION \"{_conversation.Name}\" ({_conversation.Participants.Count} members)";

        Employee? otherEmployee = null;
        foreach (var employee in _conversation.Participants)
        {
            if (employee.Username == Session.Employee?.Username) continue;
            otherEmployee = employee;
            break;
        }

        return $"DIRECT CONVERSATION WITH {otherEmployee?.Username} ({otherEmployee?.Name} {otherEmployee?.Surname})";
    }

    private ConsoleColor GetHeaderColor()
    {
        return _conversation.IsGroup ? ConsoleColor.DarkMagenta : ConsoleColor.DarkCyan;
    }

    public void GetNewMessages()
    {
        var unreadMessages = MessageRepository.GetUnreadMessages(_conversation.Id).ToList();
        foreach (var message in unreadMessages) _messages.Add(message);
    }
}