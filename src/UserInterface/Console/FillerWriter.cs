using System.Text;

namespace ApertureMessenger.UserInterface.Console;

public static class FillerWriter
{
    public static void WriteFiller(char character, int count, ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        StringBuilder output = new();
        for (var i = 0; i < count; i++)
        {
            output.Append(character);
        }

        ConsoleWriter.Write(output.ToString(), color);
    }

    public static void FillLineWithCharacter(char character, ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        ConsoleWriter.WriteLine();
        WriteFiller(character, System.Console.WindowWidth, color);
    }
}