namespace ApertureMessenger.UserInterface.Interfaces;

/// <summary>
/// Interface for the help application command.
/// </summary>
public interface IHelpCommand : ICommand
{
    public void Invoke(IActionCommand[] currentContext);
    public void Invoke(IActionCommand[] currentContext, IActionCommand? specifiedCommand);
}