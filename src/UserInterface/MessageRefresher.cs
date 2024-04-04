using System.Configuration;

namespace ApertureMessenger.UserInterface;

/// <summary>
/// Handles refreshing the UI and messages in a separate thread.
/// </summary>
public static class MessageRefresher
{
    private static Thread? _refresher;

    public static void StartRefresherThread()
    {
        _refresher = new Thread(() => { Refresher(GetSecondsToSleep()); });
        _refresher.Start();
    }

    private static void Refresher(int secondsToSleep)
    {
        while (true)
        {
            Thread.Sleep(secondsToSleep * 1000);

            try
            {
                Shared.GetNewMessages();
            }
            catch (Exception)
            {
                // unsuccessful refresh can be ignored
            }
        }
    }

    private static int GetSecondsToSleep()
    {
        return int.Parse(ConfigurationManager.AppSettings.Get("RefreshSleepSeconds") ?? "3");
    }
}