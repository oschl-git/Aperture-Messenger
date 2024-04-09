using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that handles adding an employee to an already existing group conversation.
/// </summary>
public class AddEmployeeToGroup : IActionCommand
{
    public string[] Aliases { get; } = ["addtogroup", "atg"];
    public string Description => "Adds an employee to a group conversation.";

    public Tuple<string, string>[] Arguments { get; } =
    [
        new Tuple<string, string>("conversationId*", "id of the group conversation"),
        new Tuple<string, string>("employeeUsername*", "username of the employee to add")
    ];

    public void Invoke(string[] args)
    {
        if (args.Length < 2)
        {
            Shared.Feedback = new CommandFeedback(
                "Missing arguments: You must specify the conversation ID and an username.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        int conversationId;
        try
        {
            conversationId = int.Parse(args[0]);
        }
        catch (FormatException)
        {
            Shared.Feedback = new CommandFeedback(
                "Conversation ID must be int.", CommandFeedback.FeedbackType.Error
            );
            return;
        }

        var username = args[1];

        try
        {
            ConversationRepository.AddEmployeeToGroup(new AddEmployeeToGroupRequest(conversationId, username));
        }
        catch (ConversationNotGroup)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided conversation is not a group conversation.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }
        catch (EmployeeAlreadyInConversation)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided employee is already part of the group.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }
        catch (ConversationNotFound)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided conversation does not exist.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }
        catch (EmployeeDoesNotExist)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided employee does not exist.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        if (Shared.View is ConversationListView conversationListView) conversationListView.RefreshConversations();

        Shared.Feedback = new CommandFeedback(
            $"Successfully added {username} to group conversation {conversationId}!",
            CommandFeedback.FeedbackType.Success
        );
    }
}