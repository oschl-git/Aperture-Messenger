namespace ApertureMessenger.UserInterface.Interfaces;

/// <summary>
/// Interface for application commands.
/// </summary>
public interface ICommand
{
    string[] Aliases { get; }
    public void Invoke(string[] args);
}