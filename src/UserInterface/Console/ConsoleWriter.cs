namespace ApertureMessenger.UserInterface.Console;

public static class ConsoleWriter
{
    public static void Write(string content = "", ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        System.Console.ForegroundColor = color;
        System.Console.Write(content);
        System.Console.ForegroundColor = ConsoleColors.DefaultForegroundColor;
    }

    public static void WriteLine(string content = "", ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        Write(content + "\n", color);
    }

    public static void WriteWithWordWrap(string content = "", ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        var words = content.Split(' ');

        var currentLength = 0;
        var maxLength = System.Console.WindowWidth;

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
    
    public static void Clear()
    {
        System.Console.Clear();
    }

    public static void MoveCursorToBottom(int offset = 1)
    {
        System.Console.SetCursorPosition(0, System.Console.WindowHeight - offset);
    }
}