using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Help;

/// <summary>
/// A command that handles displaying help information.
/// </summary>
public class HelpCommand : IHelpCommand
{
    public string[] Aliases { get; } = ["help", "h", "man", "manual"];
    public string Description => "Displays help for commands in the current context or for the specified command.";
    public Tuple<string, string>[] Arguments { get; } = [
        new Tuple<string, string>("command?", "optional name of a specific command to get help for")
    ];

    public void Invoke(IActionCommand[] currentContext)
    {
        SetCommandFeedback();
        Shared.View = new HelpView(currentContext);
    }
    
    public void Invoke(IActionCommand[] currentContext, IActionCommand? specifiedCommand)
    {
        if (specifiedCommand == null)
        {
            Shared.Feedback = new CommandFeedback(
                "Can't display help for a command that doesn't exist.", CommandFeedback.FeedbackType.Error
            );
            return;
        }
        
        SetCommandFeedback();
        Shared.View = new HelpView(currentContext, specifiedCommand);
    }

    private static void SetCommandFeedback()
    {
        Shared.Feedback = new CommandFeedback(
            "Use :exit to return back to the previous context.",
            CommandFeedback.FeedbackType.Info
        );
    }
}