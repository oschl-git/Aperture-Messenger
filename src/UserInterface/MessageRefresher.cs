using ApertureMessenger.UserInterface.Messaging.InterfaceHandlers;

namespace ApertureMessenger.UserInterface;

public static class MessageRefresher
{
    private static Thread refresher;

    public static void StartRefresherThread()
    {
        refresher = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(2 * 1000);

                if (SharedData.InterfaceHandler is ConversationInterfaceHandler handler)
                {
                    handler.GetNewMessages();
                }
                
                SharedData.InterfaceHandler.DrawUserInterface();
            }
        });
        refresher.Start();
    }
}