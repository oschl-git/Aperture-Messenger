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
                    Shared.Feedback = new CommandFeedback(
                        "Commands must start with a colon (:).",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Feedback = new CommandFeedback(
                        "The provided input is not a valid command in this context.",
                        CommandFeedback.FeedbackType.Error
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

        foreach (var conversation in _conversations) ComponentWriter.WriteConversation(conversation);

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
}