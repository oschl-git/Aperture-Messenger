using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.InterfaceHandlers;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class Conversation : ICommand
{
    public string[] Aliases { get; } = ["conversation"];
    public void Invoke(string[] args)
    {
        var conversationId = int.Parse(args[0]);
        var conversation = ConversationRepository.GetConversationById(conversationId);
        
        SharedData.InterfaceHandler = new ConversationInterfaceHandler(conversation);
        SharedData.InterfaceHandler.Process();
    }
}