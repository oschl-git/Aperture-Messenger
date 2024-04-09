using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;

namespace ApertureMessenger.UserInterface.Messaging.Commands;

/// <summary>
/// A command that switches to the employee list view and shows all employees.
/// </summary>
public class ListAllEmployees : IActionCommand
{
    public string[] Aliases { get; } = ["listallemployees", "allemployees", "le", "lae"];
    public string Description => "Lists all employees.";
    public Tuple<string, string>[] Arguments { get; } = [];

    public void Invoke(string[] args)
    {
        Shared.View = new EmployeeListView(EmployeeListView.EmployeeType.All);
    }
}