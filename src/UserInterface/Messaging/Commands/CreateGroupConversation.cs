using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Exceptions;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class CreateGroupConversation : ICommand
{
    public string[] Aliases { get; } = ["creategroup", "cg"];
    public void Invoke(string[] args)
    {
        if (args.Length < 3)
        {
            Shared.CommandResponse = new CommandResponse(
                "Missing arguments: You must specify the name and at least two other participants.",
                CommandResponse.ResponseType.Error
            );
            return;
        }

        var name = args[0];
        var participants = args.Skip(1).Distinct().ToArray();

        if (name.Length > 64)
        {
            Shared.CommandResponse = new CommandResponse(
                "Name can't be longer than 64 characters.",
                CommandResponse.ResponseType.Error
            );
            return;
        }
        
        switch (participants.Length)
        {
            case < 2:
                Shared.CommandResponse = new CommandResponse(
                    "Missing arguments: You must specify at least two unique participants, you moron!",
                    CommandResponse.ResponseType.Error
                );
                return;
            case > 10:
                Shared.CommandResponse = new CommandResponse(
                    "Group conversations cannot have more than 10 participants.",
                    CommandResponse.ResponseType.Error
                );
                return;
        }

        foreach (var participant in participants)
        {
            if (participant == Session.Employee?.Username)
            {
                Shared.CommandResponse = new CommandResponse(
                    "You can't specify yourself as one of the participants.",
                    CommandResponse.ResponseType.Error
                );
                return;
            }
        }

        try
        {
            ConversationRepository.CreateGroupConversation(new CreateGroupConversationRequest(name, participants));
        }
        catch (EmployeesDoNotExist e)
        {
            Shared.CommandResponse = new CommandResponse(
                $"Employees do not exist: {string.Join(", ", e.Usernames)}.",
                CommandResponse.ResponseType.Error
            );
            return;
        }

        if (Shared.View is ConversationListView conversationListView)
        {
            conversationListView.RefreshConversations();
        }
        
        Shared.CommandResponse = new CommandResponse(
            $"Conversation {name} successfully created!",
            CommandResponse.ResponseType.Success
        );
    }
}