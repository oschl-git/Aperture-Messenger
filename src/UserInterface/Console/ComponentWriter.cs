namespace ApertureMessenger.UserInterface.Console;

public static class ComponentWriter
{
    public static void WriteHeader(string title, ConsoleColor backgroundColor = ConsoleColor.White)
    {
        System.Console.BackgroundColor = backgroundColor;

        ConsoleWriter.Write(title, ConsoleColors.GetTextColorForBackground(backgroundColor));
        FillerWriter.WriteFiller(' ', System.Console.WindowWidth - title.Length);
        ConsoleWriter.WriteLine();

        System.Console.BackgroundColor = ConsoleColors.DefaultBackgroundColor;
    }

    public static void WriteUserInput(string prompt = ">")
    {
        var commandResponseLength =
            $"{SharedData.CommandResponse?.GetTypeSymbol()} {SharedData.CommandResponse?.Response}".Length;
        var commandResponseBackgroundColor = SharedData.CommandResponse?.GetTypeConsoleColor() ?? ConsoleColor.White;

        System.Console.BackgroundColor = commandResponseBackgroundColor;

        ConsoleWriter.MoveCursorToBottom(2);
        ConsoleWriter.Write(
            $" {SharedData.CommandResponse?.GetTypeSymbol()} {SharedData.CommandResponse?.Response}",
            ConsoleColors.GetTextColorForBackground(commandResponseBackgroundColor)
        );
        FillerWriter.WriteFiller(' ', System.Console.WindowWidth - commandResponseLength);
        ConsoleWriter.WriteLine();

        System.Console.BackgroundColor = ConsoleColors.DefaultBackgroundColor;
        ConsoleWriter.MoveCursorToBottom();
        ConsoleWriter.Write($"{prompt}", ConsoleColor.White);
        ConsoleWriter.Write($" {SharedData.UserInput}");
    }
}