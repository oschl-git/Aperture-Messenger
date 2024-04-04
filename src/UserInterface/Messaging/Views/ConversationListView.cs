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
    public enum ConversationType
    {
        All,
        Direct,
        Group
    }
    
    private Conversation[] _conversations;
    private ConversationType _type;

    public ConversationListView(ConversationType type)
    {
        _conversations = GetConversations(type);
        _type = type;
    }

    private static Conversation[] GetConversations(ConversationType type)
    {
        switch (type)
        {
            case ConversationType.Direct:
                return ConversationRepository.GetDirectConversations();
            case ConversationType.Group:
                return ConversationRepository.GetGroupConversations();
            case ConversationType.All:
            default:
                return ConversationRepository.GetAllConversations();
        }
    }

    public void RefreshConversations()
    {
        _conversations = GetConversations(_type);
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

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader(GetHeaderContent(), GetHeaderColor());
        ConsoleWriter.WriteLine();

        foreach (var conversation in _conversations) PrintConversation(conversation);

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }

    private string GetHeaderContent()
    {
        switch (_type)
        {
            case ConversationType.Direct:
                return "RECENT DIRECT CONVERSATIONS";
            case ConversationType.Group:
                return "RECENT GROUP CONVERSATIONS";
            case ConversationType.All:
            default:
                return "RECENT CONVERSATIONS";
        }
    }

    private ConsoleColor GetHeaderColor()
    {
        switch (_type)
        {
            case ConversationType.Direct:
                return ConsoleColor.DarkCyan;
            case ConversationType.Group:
                return ConsoleColor.DarkMagenta;
            case ConversationType.All:
            default:
                return ConsoleColor.DarkYellow;
        }
    }

    private void PrintConversation(Conversation conversation)
    {
        if (conversation.IsGroup)
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
        else
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

    private static Employee GetOtherParticipant(Employee[]? participants)
    {
        if (participants != null)
            foreach (var participant in participants)
                if (participant.Username != Session.Employee?.Username)
                    return participant;

        throw new InvalidDataException("Direct conversation had an invalid participant list.");
    }
}