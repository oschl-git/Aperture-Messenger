namespace ApertureMessenger.UserInterface.Console;

public static class ConsoleReader
{
    public static string ReadCommandFromUser()
    {
        ConsoleKeyInfo key;
        do
        {
            key = System.Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && Shared.UserInput.Length > 0)
            {
                Shared.UserInput = Shared.UserInput[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                Shared.UserInput += key.KeyChar;
            }

            Shared.RefreshView();
        } while (key.Key != ConsoleKey.Enter || Shared.UserInput.Length <= 0);

        var command = Shared.UserInput;
        Shared.UserInput = "";
        return command;
    }
}