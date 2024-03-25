using System.Text;
using ApertureMessenger.AlmsConnection.Objects;

namespace ApertureMessenger.CLI;

public static class UserInterface
{
    private static string _command = "";
    private static string? _commandResponse;
    private static string? _commandOutput = null;
    private static int? _conversationId = null;
    private static bool _conversationDisplay = false;
    private static List<Message> _messages = [];
    
    public static void Process()
    {
        while (true)
        {
            ReadCommandFromUser();
        }
    }
    
    private static void DrawUserInterface()
    {
        Console.Clear();
        
        Console.SetCursorPosition(0, Console.WindowHeight - 3);
        Console.Write(GetScreenFillWithCharacter('―'));        
        
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.Write(_commandResponse ?? GetScreenFillWithCharacter('―'));
        
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.Write($"> {_command}");
    }

    private static void ReadCommandFromUser()
    {
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && _command.Length > 0)
            {
                _command = _command[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                _command += key.KeyChar;
            }

            DrawUserInterface();
        } while (key.Key != ConsoleKey.Enter);
    }

    private static string GetScreenFillWithCharacter(char character)
    {
        StringBuilder output = new();
        for (var i = 0; i < Console.WindowWidth; i++)
        {
            output.Append(character);
        }

        return output.ToString();
    }
}