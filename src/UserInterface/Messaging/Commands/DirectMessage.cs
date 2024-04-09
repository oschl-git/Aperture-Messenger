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
public class DirectMessage : IActionCommand
{
    public string[] Aliases { get; } = ["directmessage", "dm"];
    public string Description => "Enters a direct conversation with another employee.";

    public Tuple<string, string>[] Arguments { get; } =
    [
        new Tuple<string, string>("username*", "username of the employee to message")
    ];

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

        Shared.Feedback = new CommandFeedback(
            "Input is sent as a message unless you prefix it with a colon (:).",
            CommandFeedback.FeedbackType.Info
        );
        Shared.View = new ConversationView(conversation);
    }
}