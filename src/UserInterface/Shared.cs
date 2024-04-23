using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Help;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface;

/// <summary>
/// Values shared between views.
/// </summary>
public static class Shared
{
    private static readonly object ViewLocker = new();
    public static IView View { get; set; } = new ConnectionView();

    public static string UserInput { get; set; } = "";
    public static CommandFeedback Feedback { get; set; } = new("", CommandFeedback.FeedbackType.Info);
    
    public static List<string> InputHistory { get; set; } = [];
    public static int HistoryDepth = 0;

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
            if (View is ConversationView conversationView) conversationView.GetNewMessages();
            if (View is HelpView) return;
            View.DrawUserInterface();
        }
    }
}