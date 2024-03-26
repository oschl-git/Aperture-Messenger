namespace ApertureMessenger.UserInterface.Console;

public static class ConsoleColors
{
    public const ConsoleColor DefaultForegroundColor = ConsoleColor.White;
    public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

    public static void Setup()
    {
        System.Console.ForegroundColor = DefaultForegroundColor;
        System.Console.BackgroundColor = DefaultBackgroundColor;
    }
    
    public static ConsoleColor GetTextColorForBackground(ConsoleColor backgroundColor)
    {
        ConsoleColor[] darkBackgrounds =
        [
            ConsoleColor.Black
        ];

        return Array.Exists(darkBackgrounds, color => color == backgroundColor)
            ? ConsoleColor.White
            : ConsoleColor.Black;
    }
}