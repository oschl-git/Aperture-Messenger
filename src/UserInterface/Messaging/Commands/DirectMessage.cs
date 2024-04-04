using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that handles entering a direct conversation with another user.
/// </summary>
public class DirectMessage : ICommand
{
    public string[] Aliases { get; } = ["directmessage", "dm"];

    public void Invoke(string[] args)
    {
        if (args.Length == 0)
        {
            Shared.Feedback = new CommandFeedback(
                "Missing argument: You must provide a username of the user to message.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        if (args.Length > 1)
        {
            Shared.Feedback = new CommandFeedback(
                "Too many arguments for command.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        var username = args[0];

        if (username == Session.Employee?.Username)
        {
            Shared.Feedback = new CommandFeedback(
                "You can't message yourself, you moron!",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        Conversation conversation;
        try
        {
            conversation = ConversationRepository.GetDirectConversation(args[0]);
        }
        catch (EmployeeDoesNotExist)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided username doesn't exist.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        Shared.View = new ConversationView(conversation);
    }
}