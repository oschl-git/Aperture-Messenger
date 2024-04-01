namespace ApertureMessenger.UserInterface.Console;

public static class ComponentWriter
{
    public enum StepStates
    {
        Scheduled,
        Started,
        Completed,
    }

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
        ConsoleWriter.WriteLine();
        
        var commandResponseLength =
            $" {SharedData.CommandResponse?.GetTypeSymbol()} {SharedData.CommandResponse?.Response}".Length;
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
        ConsoleWriter.Write(prompt);
        ConsoleWriter.WriteWithWordWrap($" {SharedData.UserInput}", firstLineOffset: prompt.Length);
    }

    public static void WriteStep(
        string description,
        int currentStage,
        int stepStage,
        int stepCompletedStage 
    )
    {
        StepStates state;
        
        if (currentStage == stepStage)
        {
            state = StepStates.Started;
        }
        else if (currentStage >= stepCompletedStage)
        {
            state = StepStates.Completed;
        }
        else
        {
            state = StepStates.Scheduled;
        }
        
        var color = state switch
        {
            StepStates.Started => ConsoleColor.Cyan,
            StepStates.Completed => ConsoleColor.Green,
            _ => ConsoleColor.White
        };

        var prefix = state switch
        {
            StepStates.Started => "[\u2192]",
            StepStates.Completed => "[\u2713]",
            _ => "[ ]"
        };

        ConsoleWriter.WriteLine($"{prefix} {description}", color);
    }
}