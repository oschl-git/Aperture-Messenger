namespace ApertureMessenger.UserInterface.Console;

/// <summary>
/// Handles dealing with console colors.
/// </summary>
public static class ConsoleColors
{
    public const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
    public const ConsoleColor DefaultBackgroundColor = ConsoleColor.DarkGray;

    public static void Setup()
    {
        System.Console.ForegroundColor = DefaultForegroundColor;
        System.Console.BackgroundColor = DefaultBackgroundColor;
    }

    public static ConsoleColor GetTextColorForBackground(ConsoleColor backgroundColor)
    {
        ConsoleColor[] darkBackgrounds =
        [
            ConsoleColor.Black,
            ConsoleColor.Blue,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkGray,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkYellow
        ];

        return Array.Exists(darkBackgrounds, color => color == backgroundColor)
            ? ConsoleColor.White
            : ConsoleColor.Black;
    }
}