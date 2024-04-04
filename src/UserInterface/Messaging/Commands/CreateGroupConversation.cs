using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that handles creating a new group conversation.
/// </summary>
public class CreateGroupConversation : ICommand
{
    public string[] Aliases { get; } = ["creategroup", "cg"];

    public void Invoke(string[] args)
    {
        if (args.Length < 3)
        {
            Shared.Feedback = new CommandFeedback(
                "Missing arguments: You must specify the name and at least two other participants.",
                CommandFeedback.ResponseType.Error
            );
            return;
        }

        var name = args[0];
        var participants = args.Skip(1).Distinct().ToArray();

        if (name.Length > 16)
        {
            Shared.Feedback = new CommandFeedback(
                "Name can't be longer than 16 characters.",
                CommandFeedback.ResponseType.Error
            );
            return;
        }

        switch (participants.Length)
        {
            case < 2:
                Shared.Feedback = new CommandFeedback(
                    "Missing arguments: You must specify at least two unique participants, you moron!",
                    CommandFeedback.ResponseType.Error
                );
                return;
            case > 9:
                Shared.Feedback = new CommandFeedback(
                    "Group conversations cannot have more than 10 participants.",
                    CommandFeedback.ResponseType.Error
                );
                return;
        }

        foreach (var participant in participants)
            if (participant == Session.Employee?.Username)
            {
                Shared.Feedback = new CommandFeedback(
                    "You can't specify yourself as one of the participants.",
                    CommandFeedback.ResponseType.Error
                );
                return;
            }

        try
        {
            ConversationRepository.CreateGroupConversation(new CreateGroupConversationRequest(name, participants));
        }
        catch (EmployeesDoNotExist e)
        {
            Shared.Feedback = new CommandFeedback(
                $"Employees do not exist: {string.Join(", ", e.Usernames)}.",
                CommandFeedback.ResponseType.Error
            );
            return;
        }

        if (Shared.View is ConversationListView conversationListView) conversationListView.RefreshConversations();

        Shared.Feedback = new CommandFeedback(
            $"Conversation {name} successfully created!",
            CommandFeedback.ResponseType.Success
        );
    }
}