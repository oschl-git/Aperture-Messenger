using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

/// <summary>
/// A view/UI handler for displaying a list of conversations.
/// </summary>
public class ConversationListView : IView
{
    private Conversation[] _conversations;
    private readonly bool _areGroup;

    public ConversationListView(bool areGroup)
    {
        _areGroup = areGroup;
        _conversations = GetConversations();
    }

    private Conversation[] GetConversations()
    {
        return _areGroup
            ? ConversationRepository.GetGroupConversations()
            : ConversationRepository.GetDirectConversations();
    }

    public void RefreshConversations()
    {
        _conversations = GetConversations();
        Shared.RefreshView();
    }

    public void Process()
    {
        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.Response = new CommandResponse(
                        "Commands must start with a colon (:).",
                        CommandResponse.ResponseType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Response = new CommandResponse(
                        $"{userInput} is not a valid command in this context.",
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

        ComponentWriter.WriteHeader(GetHeaderContent(), GetHeaderColor());
        ConsoleWriter.WriteLine();

        if (_areGroup) PrintGroupConversations();
        else PrintDirectConversations();

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }

    private string GetHeaderContent()
    {
        return _areGroup ? "RECENT GROUP CONVERSATIONS" : "RECENT DIRECT CONVERSATIONS";
    }

    private ConsoleColor GetHeaderColor()
    {
        return _areGroup ? ConsoleColor.DarkMagenta : ConsoleColor.DarkCyan;
    }

    private void PrintGroupConversations()
    {
        foreach (var conversation in _conversations)
        {
            if (conversation.Name == null || conversation.Participants == null)
                throw new InvalidDataException("Group conversations didn't have required attributes.");

            ConsoleWriter.Write(" - ");
            ConsoleWriter.Write(conversation.Name, ConsoleColor.Magenta);
            ConsoleWriter.Write(", ID: ");
            ConsoleWriter.Write(conversation.Id.ToString(), ConsoleColor.Green);
            ConsoleWriter.Write(
                $" ({conversation.Participants.Count} members, " +
                $"last updated: [{conversation.DateTimeUpdated.ToLocalTime()}])"
            );
            ConsoleWriter.WriteLine();
        }
    }

    private void PrintDirectConversations()
    {
        foreach (var conversation in _conversations)
        {
            if (conversation.Participants == null)
                throw new InvalidDataException("Direct conversations didn't have required attributes.");

            ConsoleWriter.Write(" - ");

            var otherParticipant = GetOtherParticipant(conversation.Participants.ToArray());
            ConsoleWriter.Write(
                $"DM with {otherParticipant.Username} ({otherParticipant.Name} {otherParticipant.Surname})",
                ConsoleColor.Cyan
            );
            ConsoleWriter.Write(", ID: ");
            ConsoleWriter.Write(conversation.Id.ToString(), ConsoleColor.Green);
            ConsoleWriter.Write(
                $" (last updated: [{conversation.DateTimeUpdated.ToLocalTime()}])"
            );
            ConsoleWriter.WriteLine();
        }
    }

    private Employee GetOtherParticipant(Employee[]? participants)
    {
        if (participants != null)
            foreach (var participant in participants)
                if (participant.Username != Session.Employee?.Username)
                    return participant;

        throw new InvalidDataException("Direct conversation had an invalid participant list.");
    }
}