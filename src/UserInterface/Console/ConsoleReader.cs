namespace ApertureMessenger.UserInterface.Console;

public static class ConsoleReader
{
    public static string ReadCommandFromUser()
    {
        ConsoleKeyInfo key;
        do
        {
            key = System.Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && SharedData.UserInput.Length > 0)
            {
                SharedData.UserInput = SharedData.UserInput[..^1];
            }
            else if (!char.IsControl(key.KeyChar))
            {
                SharedData.UserInput += key.KeyChar;
            }

            SharedData.InterfaceHandler.DrawUserInterface();
        } while (key.Key != ConsoleKey.Enter || SharedData.UserInput.Length <= 0);

        var command = SharedData.UserInput;
        SharedData.UserInput = "";
        return command;
    }
}