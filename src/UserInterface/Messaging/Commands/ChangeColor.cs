using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that changes the current user's display colour.
/// </summary>
public class ChangeColor : IActionCommand
{
    public string[] Aliases { get; } = ["changecolor", "changecolour", "cc"];
    public string Description => "Change your display colour.";

    public Tuple<string, string>[] Arguments { get; } =
    [
        new Tuple<string, string>("colorNumber*", "number of the desired colour")
    ];

    public void Invoke(string[] args)
    {
        if (args.Length < 1)
        {
            Shared.Feedback = new CommandFeedback(
                "Missing arguments: You must specify the number of the desired colour.",
                CommandFeedback.FeedbackType.Error
            );
            return;
        }

        int colorNumber;
        try
        {
            colorNumber = int.Parse(args[0]);
        }
        catch (FormatException)
        {
            Shared.Feedback = new CommandFeedback(
                "Color number must be an integer.", CommandFeedback.FeedbackType.Error
            );
            return;
        }

        EmployeeRepository.ChangeEmployeeColor(new SetEmployeeColorRequest(colorNumber));

        Shared.Feedback = new CommandFeedback(
            "Successfully changed colour!",
            CommandFeedback.FeedbackType.Success
        );
    }
}