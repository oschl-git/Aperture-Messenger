using ApertureMessenger.UserInterface;
using ApertureMessenger.UserInterface.Console;

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
            Shared.View.Process();
        }
    }
}