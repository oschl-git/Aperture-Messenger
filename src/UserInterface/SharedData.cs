using ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface;

public static class SharedData
{
    public static IInterfaceHandler InterfaceHandler = AuthenticationInterfaceHandler.GetInstance();
    public static string UserInput  = "";
    public static CommandResponse? CommandResponse;
}