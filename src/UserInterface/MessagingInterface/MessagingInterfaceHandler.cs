namespace ApertureMessenger.UserInterface.MessagingInterface;

public class MessagingInterfaceHandler : InterfaceHandler
{
    private static readonly MessagingInterfaceHandler Instance = new();

    private MessagingInterfaceHandler()
    {
    }

    public static MessagingInterfaceHandler GetInstance()
    {
        return Instance;
    }
    
    public void Process()
    {
        SharedData.InterfaceHandler = this;
        
        // SharedData.CommandResponse = "Hello and, again, welcome to Aperture Messenger.";
        while (true)
        {
            DrawUserInterface();
            var command = ConsoleHandler.ReadCommandFromUser();
            // SharedData.CommandResponse = $"You typed: \"{command}\". Great job!";
        }
    }

    public void DrawUserInterface()
    {
        Console.Clear();

        Console.SetCursorPosition(0, Console.WindowHeight - 3);
        ConsoleHandler.FillLineWithCharacter('―', ConsoleColor.DarkCyan);

        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        // Console.Write(SharedData.CommandResponse ?? "???");

        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        ConsoleHandler.Write($"> {SharedData.UserInput}", ConsoleColor.Magenta);
    }
}