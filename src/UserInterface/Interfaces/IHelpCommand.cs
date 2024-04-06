namespace ApertureMessenger.UserInterface.Interfaces;

public interface IHelpCommand : ICommand
{
    public void Invoke(IActionCommand[] currentContext);
    public void Invoke(IActionCommand[] currentContext, IActionCommand? specifiedCommand = null);
}