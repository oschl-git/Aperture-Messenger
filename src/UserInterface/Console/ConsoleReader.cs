namespace ApertureMessenger.UserInterface.Console;

/// <summary>
/// Handles reading input from the console.
/// </summary>
public static class ConsoleReader
{
    public static string ReadCommandFromUser()
    {
        ConsoleKeyInfo key;
        do
        {
            key = System.Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Shared.InputHistory.Count <= 0) break;

                    if (Shared.InputHistory.Count > Shared.HistoryDepth) Shared.HistoryDepth++;
                    if (Shared.HistoryDepth > 0) Shared.UserInput = Shared.InputHistory[^Shared.HistoryDepth];
                    break;
                case ConsoleKey.DownArrow:
                    if (Shared.InputHistory.Count <= 0) break;

                    if (Shared.HistoryDepth > 1) Shared.HistoryDepth--;
                    if (Shared.HistoryDepth > 0) Shared.UserInput = Shared.InputHistory[^Shared.HistoryDepth];
                    break;
                case ConsoleKey.Backspace when Shared.UserInput.Length > 0:
                    if (
                        ((key.Modifiers & ConsoleModifiers.Control) != 0) ||
                        ((key.Modifiers & ConsoleModifiers.Alt) != 0)
                    )
                    {
                        Shared.UserInput = string.Join(' ', Shared.UserInput.Split(' ').SkipLast(1).ToArray());
                        break;
                    }

                    Shared.UserInput = Shared.UserInput[..^1];
                    Shared.HistoryDepth = 0;
                    break;
                default:
                {
                    if (!char.IsControl(key.KeyChar))
                    {
                        Shared.UserInput += key.KeyChar;
                    }

                    Shared.HistoryDepth = 0;

                    break;
                }
            }

            Shared.RefreshView();
        } while (key.Key != ConsoleKey.Enter || Shared.UserInput.Length <= 0);

        var command = Shared.UserInput;
        Shared.UserInput = "";
        return command;
    }
}