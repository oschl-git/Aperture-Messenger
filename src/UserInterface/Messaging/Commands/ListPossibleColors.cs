using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the color list view and shows the user possible employee colors.
/// </summary>
public class ListPossibleColors : IActionCommand
{
    public string[] Aliases { get; } = ["listcolors", "listcolours", "lc"];
    public string Description => "List possible colours for employees.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.Feedback = new CommandFeedback(
            "Use the :changecolor command to change your display colour in conversations.",
            CommandFeedback.FeedbackType.Info
        );
        Shared.View = new ColorListView();
    }
}