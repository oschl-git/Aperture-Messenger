namespace ApertureMessenger.UserInterface.Interfaces;

/// <summary>
/// Interface for application commands that perform actions.
/// </summary>
public interface IActionCommand : ICommand
{
    public void Invoke(string[] args);
}