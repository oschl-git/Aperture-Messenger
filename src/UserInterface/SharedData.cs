using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface;

public static class SharedData
{
    public static IView View = AuthenticationView.GetInstance();
    public static string UserInput  = "";
    public static CommandResponse? CommandResponse;
}