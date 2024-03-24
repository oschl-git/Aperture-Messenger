using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Authentication;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;

namespace ApertureMessenger;

internal static class Program
{
    private static void Main()
    {
        Authenticator.Login(new LoginRequest("erik", "erikerikerik"));
    }
}