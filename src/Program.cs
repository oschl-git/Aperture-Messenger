using ApertureMessenger.AlmsConnection.Authentication;
using ApertureMessenger.AlmsConnection.Repositories;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        Authenticator.Login("erik", "erikerikerik");
    }
}