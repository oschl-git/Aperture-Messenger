using ApertureMessenger.AlmsConnection.Authentication;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        Authenticator.Login("erik", "erikerikerik");
    }
}