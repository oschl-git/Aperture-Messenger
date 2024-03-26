using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Messaging;

public class MessagingInterfaceHandler : IInterfaceHandler
{
    private static readonly MessagingInterfaceHandler Instance = new();

    private MessagingInterfaceHandler()
    {
    }

    public static MessagingInterfaceHandler GetInstance()
    {
        return Instance;
    }
    
    public void Process()
    {
        throw new NotImplementedException();
    }

    public void DrawUserInterface()
    {
        throw new NotImplementedException();
    }
}