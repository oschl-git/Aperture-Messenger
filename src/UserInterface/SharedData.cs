using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.UserInterface.AuthenticationInterface;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface;

public static class SharedData
{
    public static InterfaceHandler InterfaceHandler = AuthenticationInterfaceHandler.GetInstance();
    public static string UserInput  = "";
    public static CommandResponse? CommandResponse;
    public static string? CommandOutput = null;
    public static int? ConversationId = null;
    public static bool ConversationDisplay = false;
    public static List<Message> Messages = [];
}