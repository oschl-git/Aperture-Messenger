using System.Security.Principal;
using System.Text;

namespace ApertureMessenger.UserInterface;

public static class ConsoleHandler
{
    private const ConsoleColor DefaultColor = ConsoleColor.White;
    private const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

    public static void Setup()
    {
        Console.ForegroundColor = DefaultColor;
        Console.BackgroundColor = DefaultBackgroundColor;
    }

    public static void Write(string content = "", ConsoleColor color = DefaultColor)
    {
        Console.ForegroundColor = color;
        Console.Write(content);
        Console.ForegroundColor = DefaultColor;
    }

    public static void WriteLine(string content = "", ConsoleColor color = DefaultColor)
    {
        Write(content + "\n", color);
    }

    public static void WriteWithWordWrap(string content = "", ConsoleColor color = DefaultColor)
    {
        var words = content.Split(' ');

        var currentLength = 0;
        var maxLength = Console.WindowWidth;

        foreach (var word in words)
        {
            // Handle words longer than the window
            if (word.Length > maxLength)
            {
                var lastPrinted = 0;
                while (lastPrinted < word.Length)
                {
                    var length = Math.Min(maxLength - currentLength, word.Length - lastPrinted);
                    Write(word.Substring(lastPrinted, length), color);

                    currentLength = 0;
                    lastPrinted += length;

                    if (lastPrinted < word.Length)
                    {
                        WriteLine("", color);
                    }
                    else
                    {
                        Write(" ", color);
                        currentLength = length + 1;
                    }
                }

                continue;
            }

            // Write newline if word is too long for the current line
            if (currentLength + word.Length > maxLength)
            {
                WriteLine("", color);
                currentLength = 0;
            }

            Write(currentLength + word.Length + 1 <= maxLength ? word + " " : word, color);
            currentLength += word.Length + 1;
        }
    }
    
    public static void WriteFiller(char character, int count, ConsoleColor color = DefaultColor)
    {
        StringBuilder output = new();
        for (var i = 0; i < count; i++)
        {
            output.Append(character);
        }

        Write(output.ToString(), color);
    }

    public static void FillLineWithCharacter(char character, ConsoleColor color = DefaultColor)
    {
        WriteLine();
        WriteFiller(character, Console.WindowWidth, color);
    }

    public static void Clear()
    {
        Console.Clear();
    }

    public static void MoveCursorToBottom(int offset = 1)
    {
        Console.SetCursorPosition(0, Console.WindowHeight - offset);
    }

    public static string ReadCommandFromUser()
    {
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && SharedData.UserInput.Length > 0)
            {
                SharedData.UserInput = SharedData.UserInput[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                SharedData.UserInput += key.KeyChar;
            }

            SharedData.InterfaceHandler.DrawUserInterface();
        } while (key.Key != ConsoleKey.Enter || SharedData.UserInput.Length <= 0);

        var command = SharedData.UserInput;
        SharedData.UserInput = "";
        return command;
    }

    public static void WriteUserInput(string prompt = ">")
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;

        var commandResponseLength =
            $"{SharedData.CommandResponse?.GetTypeSymbol()} {SharedData.CommandResponse?.Response}".Length;
        var commandResponseColor = SharedData.CommandResponse?.GetTypeConsoleColor() ?? ConsoleColor.White;
        
        var userInputLineLength = $"{prompt} {SharedData.UserInput}".Length;
        
        MoveCursorToBottom(2);
        Write($"{SharedData.CommandResponse?.GetTypeSymbol()}", ConsoleColor.White);
        Write($" {SharedData.CommandResponse?.Response}", commandResponseColor);
        WriteFiller(' ', Console.WindowWidth - commandResponseLength);
        WriteLine();
        
        MoveCursorToBottom();
        Write($"{prompt}", ConsoleColor.Magenta);
        Write($" {SharedData.UserInput}", ConsoleColor.White);
        WriteFiller(' ', Console.WindowWidth - userInputLineLength);
        
        Console.BackgroundColor = DefaultBackgroundColor;
    }
}