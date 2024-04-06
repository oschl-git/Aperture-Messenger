namespace ApertureMessenger.UserInterface.Interfaces;

public interface IActionCommand : ICommand
{
    public void Invoke(string[] args);
}