using ApertureMessenger.AlmsConnection;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

public class ColorListView : IView
{
    public void Process()
    {
        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.Feedback = new CommandFeedback(
                        "Commands must start with a colon (:).",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Feedback = new CommandFeedback(
                        "The provided input is not a valid command in this context.",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.Success:
                default:
                    return;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("POSSIBLE EMPLOYEE COLOURS", ConsoleColor.Blue);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteLine("  1. BLACK", ConsoleColor.Black);
        ConsoleWriter.WriteLine("  2. DARK BLUE", ConsoleColor.DarkBlue);
        ConsoleWriter.WriteLine("  3. DARK GREEN", ConsoleColor.DarkGreen);
        ConsoleWriter.WriteLine("  4. DARK CYAN", ConsoleColor.DarkCyan);
        ConsoleWriter.WriteLine("  5. DARK RED", ConsoleColor.DarkRed);
        ConsoleWriter.WriteLine("  6. DARK MAGENTA", ConsoleColor.DarkMagenta);
        ConsoleWriter.WriteLine("  7. DARK YELLOW", ConsoleColor.DarkYellow);
        ConsoleWriter.WriteLine("  8. GRAY", ConsoleColor.Gray);
        ConsoleWriter.WriteLine("  9. DARK GRAY", ConsoleColor.DarkGray);
        ConsoleWriter.WriteLine(" 10. BLUE", ConsoleColor.Blue);
        ConsoleWriter.WriteLine(" 11. GREEN", ConsoleColor.Green);
        ConsoleWriter.WriteLine(" 12. CYAN", ConsoleColor.Cyan);
        ConsoleWriter.WriteLine(" 13. RED", ConsoleColor.Red);
        ConsoleWriter.WriteLine(" 14. MAGENTA", ConsoleColor.Magenta);
        ConsoleWriter.WriteLine(" 15. YELLOW", ConsoleColor.Yellow);
        ConsoleWriter.WriteLine(" 16. WHITE");

        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine(
            "Note that colours are displayed differently on different systems and environments."
        );
        
        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }
}