using ApertureMessenger.UserInterface.Authentication.Views;
using ApertureMessenger.UserInterface.Interfaces;

namespace ApertureMessenger.UserInterface.ErrorHandling.Commands;

public class Retry : ICommand
{
    public string[] Aliases { get; } = ["retry", "restart", "continue", "r", "c"];
    public void Invoke(string[] args)
    {
        Shared.View = new ConnectionView();
    }
}