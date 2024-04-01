using ApertureMessenger.UserInterface;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.ErrorHandling.Views;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        ConsoleColors.Setup();
        ConsoleWriter.Setup();
        MessageRefresher.StartRefresherThread();

        while (true)
        {
            try
            {
                Shared.View.Process();
            }
            catch (Exception e)
            {
                Shared.View = new ErrorView(e);
            }
        }
    }
}