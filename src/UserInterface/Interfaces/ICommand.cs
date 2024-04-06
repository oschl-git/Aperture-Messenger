namespace ApertureMessenger.UserInterface.Interfaces;

/// <summary>
/// Interface for application commands.
/// </summary>
public interface ICommand
{
    public string[] Aliases { get; }
    public string Description { get; }
    public Tuple<string, string>[] Arguments { get; }
}