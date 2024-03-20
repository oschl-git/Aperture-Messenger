using System.Text.Json;
using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Authentication;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine(Authenticator.Login("erik", "erikerikerik"));
    }
}