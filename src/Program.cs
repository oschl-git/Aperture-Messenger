using ApertureMessenger.UserInterface;
using ApertureMessenger.UserInterface.AuthenticationInterface;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        ConsoleHandler.Setup();
        SharedData.InterfaceHandler.Process();
    }
}