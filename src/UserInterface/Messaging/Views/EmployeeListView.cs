using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Objects;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Messaging.Views;

/// <summary>
/// A view/UI handler for displaying a list of employees.
/// </summary>
public class EmployeeListView : IView
{
    public enum EmployeeType
    {
        All,
        Online
    }
    
    private Employee[] _employees;
    private EmployeeType _type;

    public EmployeeListView(EmployeeType type)
    {
        _employees = GetEmployees(type);
        _type = type;
    }

    private static Employee[] GetEmployees(EmployeeType type)
    {
        switch (type)
        {
            case EmployeeType.Online:
                return EmployeeRepository.GetOnlineEmployees();
            default:
                return EmployeeRepository.GetAllEmployees();
        }
    }

    public void Process()
    {
        while (true)
        {
            Shared.RefreshView();

            var userInput = ConsoleReader.ReadCommandFromUser();
            var commandResult = CommandProcessor.InvokeCommand(userInput, GlobalCommands.Commands);
            switch (commandResult)
            {
                case CommandProcessor.Result.NotACommand:
                    Shared.Feedback = new CommandFeedback(
                        "Commands must start with a colon (:).",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.InvalidCommand:
                    Shared.Feedback = new CommandFeedback(
                        "The provided input is not a valid command in this context.",
                        CommandFeedback.FeedbackType.Error
                    );
                    break;
                case CommandProcessor.Result.Success:
                default:
                    return;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader(GetHeaderContent(), GetHeaderColor());
        ConsoleWriter.WriteLine();

        if (_employees.Length <= 0) ConsoleWriter.Write("No employees to display.", ConsoleColor.Red);
        else foreach (var employee in _employees) ComponentWriter.WriteEmployee(employee);

        ComponentWriter.WriteUserInput($"{Session.Employee?.Username}>");
    }

    private string GetHeaderContent()
    {
        switch (_type)
        {
            case EmployeeType.Online:
                return "ONLINE EMPLOYEES";
            default:
                return "ALL EMPLOYEES";
        }
    }

    private ConsoleColor GetHeaderColor()
    {
        switch (_type)
        {
            case EmployeeType.Online:
                return ConsoleColor.DarkGreen;
            default:
                return ConsoleColor.DarkCyan;
        }
    }

    public void RefreshUnreadConversations()
    {
        _employees = GetEmployees(_type);
    }
}