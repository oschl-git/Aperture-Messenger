using ApertureMessenger.UserInterface.Messaging.Views;

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

                if (SharedData.View is ConversationView handler)
                {
                    handler.GetNewMessages();
                }
                
                SharedData.View.DrawUserInterface();
            }
        });
        refresher.Start();
    }
}