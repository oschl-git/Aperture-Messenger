using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that handles entering a conversation by ID.
/// </summary>
public class ConversationById : ICommand
{
    public string[] Aliases { get; } = ["conversationid", "cid"];

    public void Invoke(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                Shared.Feedback = new CommandFeedback(
                    "Missing argument: You must provide the ID of the conversation.",
                    CommandFeedback.FeedbackType.Error
                );
                return;
            case > 1:
                Shared.Feedback = new CommandFeedback(
                    "Too many arguments for command.",
                    CommandFeedback.FeedbackType.Error
                );
                return;
        }

        Conversation conversation;
        try
        {
            conversation = ConversationRepository.GetConversationById(int.Parse(args[0]));
        }
        catch (ConversationNotFound)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided conversation doesn't exist.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }
        catch (FormatException)
        {
            Shared.Feedback = new CommandFeedback(
                "The provided conversation ID must be an integer.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        Shared.View = new ConversationView(conversation);
    }
}