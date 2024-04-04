using System.Text;

namespace ApertureMessenger.UserInterface.Console;

/// <summary>
/// Handles writing filler to the console.
/// </summary>
public static class FillerWriter
{
    public static void WriteFiller(char character, int count, ConsoleColor color = ConsoleColors.DefaultForegroundColor)
    {
        StringBuilder output = new();
        for (var i = 0; i < count; i++) output.Append(character);

        ConsoleWriter.Write(output.ToString(), color);
    }
}