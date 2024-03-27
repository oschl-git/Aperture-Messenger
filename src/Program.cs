using ApertureMessenger.UserInterface;
using ApertureMessenger.UserInterface.Console;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        ConsoleColors.Setup();
        while (true)
        {
            SharedData.InterfaceHandler.Process();
        }
    }
}