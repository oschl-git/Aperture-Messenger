namespace ApertureMessenger.UserInterface.AuthenticationInterface;

public class AuthenticationInterfaceHandler : InterfaceHandler
{
    private static readonly AuthenticationInterfaceHandler Instance = new();

    private AuthenticationInterfaceHandler()
    {
    }

    public static AuthenticationInterfaceHandler GetInstance()
    {
        return Instance;
    }
    
    public void Process()
    {
        SharedData.InterfaceHandler = this;

        while (true)
        {
            DrawUserInterface();
            var command = ConsoleHandler.ReadCommandFromUser();
        }
    }

    public void DrawUserInterface()
    {
        ConsoleHandler.Clear();
        
        ConsoleHandler.WriteWithWordWrap("APERTURE MESSENGER ALMS AUTHENTICATION", ConsoleColor.Cyan);
        ConsoleHandler.FillLineWithCharacter('―');
        
        ConsoleHandler.WriteWithWordWrap("Hello and, again, welcome to Aperture Messenger.", ConsoleColor.DarkCyan);
        ConsoleHandler.WriteLine();
        ConsoleHandler.WriteWithWordWrap("ALMS (ASAP) authentication is required to access Aperture Intelligence services. Please, log in or register a new employee account.");
        
        ConsoleHandler.WriteLine();
        ConsoleHandler.WriteLine();
        ConsoleHandler.WriteWithWordWrap("Available authentication commands:", ConsoleColor.Yellow);
        ConsoleHandler.WriteLine();
        ConsoleHandler.WriteWithWordWrap(":login");
        ConsoleHandler.WriteLine();
        ConsoleHandler.WriteWithWordWrap(":register");
        
        ConsoleHandler.WriteUserInput();
    }
}