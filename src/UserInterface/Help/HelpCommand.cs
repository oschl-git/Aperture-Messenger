using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.Help;

public class HelpCommand : IHelpCommand
{
    public string[] Aliases { get; } = ["help", "h", "man", "manual"];
    public string Description => "Displays help for commands in the current context or for the specified command.";
    public Tuple<string, string>[] Arguments { get; } = [
        new Tuple<string, string>("command?", "optional name of a specific command to get help for")
    ];

    public void Invoke(IActionCommand[] currentContext, IActionCommand? specifiedCommand = null)
    {
        Shared.View = new HelpView(currentContext, specifiedCommand);
    }
}