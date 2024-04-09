using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the employee list view and shows online employees.
/// </summary>
public class ListOnlineEmployees : IActionCommand
{
    public string[] Aliases { get; } = ["listonlineemployees", "onlineemployees", "lo", "loe"];
    public string Description => "Lists online employees.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new EmployeeListView(EmployeeListView.EmployeeType.Online);
    }
}