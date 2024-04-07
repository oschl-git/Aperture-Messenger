using System.Text;
using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Objects;

namespace ApertureMessenger.UserInterface.Console;

/// <summary>
/// Handles writing CLI components to the console.
/// </summary>
public static class ComponentWriter
{
    private enum StepState
    {
        Scheduled,
        Started,
        Completed
    }

    public static void WriteHeader(string title, ConsoleColor backgroundColor = ConsoleColor.White)
    {
        System.Console.BackgroundColor = backgroundColor;

        title = $" {title}";
        ConsoleWriter.Write(title, ConsoleColors.GetTextColorForBackground(backgroundColor));
        FillerWriter.WriteFiller(' ', System.Console.WindowWidth - title.Length);
        ConsoleWriter.WriteLine();

        System.Console.BackgroundColor = ConsoleColors.DefaultBackgroundColor;
    }

    public static void WriteUserInput(string prompt = ">", bool obfuscate = false)
    {
        ConsoleWriter.WriteLine();

        var commandResponseLength =
            $" {Shared.Feedback.GetTypeSymbol()} {Shared.Feedback.Response}".Length;
        var commandResponseBackgroundColor = Shared.Feedback.GetTypeConsoleColor();

        System.Console.BackgroundColor = commandResponseBackgroundColor;

        ConsoleWriter.MoveCursorToBottom(2);
        ConsoleWriter.Write(
            $" {Shared.Feedback.GetTypeSymbol()} {Shared.Feedback.Response}",
            ConsoleColors.GetTextColorForBackground(commandResponseBackgroundColor)
        );
        FillerWriter.WriteFiller(' ', System.Console.WindowWidth - commandResponseLength);
        ConsoleWriter.WriteLine();

        System.Console.BackgroundColor = ConsoleColors.DefaultBackgroundColor;
        ConsoleWriter.MoveCursorToBottom();
        ConsoleWriter.Write(prompt);

        var userInput = obfuscate ? ObfuscateString(Shared.UserInput) : Shared.UserInput;

        ConsoleWriter.WriteWithWordWrap($" {userInput}", firstLineOffset: prompt.Length);
    }

    public static void WriteStep(
        string description,
        int currentStage,
        int stepStage,
        int stepCompletedStage
    )
    {
        StepState state;

        if (currentStage == stepStage)
            state = StepState.Started;
        else if (currentStage >= stepCompletedStage)
            state = StepState.Completed;
        else
            state = StepState.Scheduled;

        var color = state switch
        {
            StepState.Started => ConsoleColor.Cyan,
            StepState.Completed => ConsoleColor.Green,
            _ => ConsoleColor.White
        };

        var prefix = state switch
        {
            StepState.Started => "[\u2192]",
            StepState.Completed => "[\u2713]",
            _ => "[ ]"
        };

        ConsoleWriter.WriteLine($"{prefix} {description}", color);
    }

    private static string ObfuscateString(string value, char obfuscator = '*')
    {
        var output = new StringBuilder();

        for (var i = 0; i < value.Length; i++) output.Append(obfuscator);

        return output.ToString();
    }
    
    public static void WriteConversation(Conversation conversation)
    {
        if (conversation.IsGroup)
        {
            if (conversation.Name == null || conversation.Participants == null)
                throw new InvalidDataException("Group conversations didn't have required attributes.");

            ConsoleWriter.Write(" - ");
            ConsoleWriter.Write(conversation.Name, ConsoleColor.Magenta);
            ConsoleWriter.Write(", ID: ");
            ConsoleWriter.Write(conversation.Id.ToString(), ConsoleColor.Green);
            ConsoleWriter.Write(
                $" ({conversation.Participants.Count} members, " +
                $"last updated: {conversation.DateTimeUpdated.ToLocalTime()})"
            );
        }
        else
        {
            if (conversation.Participants == null)
                throw new InvalidDataException("Direct conversations didn't have required attributes.");

            ConsoleWriter.Write(" - ");

            var otherParticipant = GetOtherConversationParticipant(conversation.Participants.ToArray());
            ConsoleWriter.Write(
                $"DM with {otherParticipant.Username} ({otherParticipant.Name} {otherParticipant.Surname})",
                ConsoleColor.Cyan
            );
            ConsoleWriter.Write(", ID: ");
            ConsoleWriter.Write(conversation.Id.ToString(), ConsoleColor.Green);
            ConsoleWriter.Write(
                $" (last updated: {conversation.DateTimeUpdated.ToLocalTime()})"
            );
        }

        if (conversation.UnreadMessageCount != null)
        {
            ConsoleWriter.Write($" [{conversation.UnreadMessageCount} new]", ConsoleColor.Red);
        }
        
        ConsoleWriter.WriteLine();
    }

    private static Employee GetOtherConversationParticipant(Employee[]? participants)
    {
        if (participants != null)
            foreach (var participant in participants)
                if (participant.Username != Session.Employee?.Username)
                    return participant;

        throw new InvalidDataException("Direct conversation had an invalid participant list.");
    }
}