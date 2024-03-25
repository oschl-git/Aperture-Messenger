namespace ApertureMessenger.CLI;

public class ConsoleWriter
{
    private const ConsoleColor DefaultColor = ConsoleColor.White;

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

    public static void PrintWithWordWrap(string content = "", ConsoleColor color = DefaultColor)
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
                    Console.Write(word.Substring(lastPrinted, length));

                    currentLength = 0;
                    lastPrinted += length;

                    if (lastPrinted < word.Length)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(' ');
                        currentLength = length + 1;
                    }
                }
                continue;
            }
            
            // Write newline is word is too long for the current line
            if (currentLength + word.Length > maxLength)
            {
                Console.WriteLine();
                currentLength = 0;
            }

            Console.Write(currentLength + word.Length + 1 <= maxLength ? word + " " : word);
            currentLength += word.Length + 1;
        }
    }
}