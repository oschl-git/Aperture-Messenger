using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface;

public static class Shared
{
    private static readonly object ViewLocker = new();
    public static IView View = new ConnectionView();
    
    public static string UserInput  = "";
    public static CommandResponse? CommandResponse;

    public static void RefreshView()
    {
        lock (ViewLocker)
        {
            View.DrawUserInterface();
        }
    }
    
    public static void GetNewMessages()
    {
        lock (ViewLocker)
        {
            if (View is ConversationView conversationView)
            {
                conversationView.GetNewMessages();
            }
            View.DrawUserInterface();
        }
    }
}