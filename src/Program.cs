using ApertureMessenger.AlmsConnection.Authentication;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface;
using ApertureMessenger.UserInterface.Console;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        ConsoleColors.Setup();
        SharedData.InterfaceHandler.Process();
    }
}