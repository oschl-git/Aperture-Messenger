namespace ApertureMessenger.UserInterface.Interfaces;

public interface ICommand
{
    string[] Aliases
    {
        get;
    }
    public void Invoke();
}