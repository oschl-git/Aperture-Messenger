namespace ApertureMessenger.UserInterface;

public static class MessageRefresher
{
    private const int SecondsToSleep = 3;
    private static Thread? _refresher;

    public static void StartRefresherThread()
    {
        _refresher = new Thread(Refresher);
        _refresher.Start();
    }

    private static void Refresher()
    {
        while (true)
        {
            Thread.Sleep(SecondsToSleep * 1000);
            
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
}