using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

public class Logout : ICommand
{
    public string[] Aliases { get; } = ["logout"];
    public void Invoke(string[] args)
    {
        Session.ClearSession();
        Shared.Response = new CommandResponse("Successfully logged out.", CommandResponse.ResponseType.Success);
        Shared.View = new AuthenticationView();
    }
}