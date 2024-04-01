using ApertureMessenger.AlmsConnection;
using ApertureMessenger.AlmsConnection.Authentication;
using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Authentication.Commands;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Messaging.Views;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

public class LoginView : IView
{
    private static readonly ICommand[] Commands =
    [
        new Exit()
    ];

    private enum Stage
    {
        UsernameInput,
        UsernameVerification,
        PasswordInput,
        LoginAttempt,
        LoginSuccess,
        LoginAborted,
    }

    private Stage _currentStage = Stage.UsernameInput;

    private string? _submittedUsername;
    private string? _submittedPassword;

    public void Process()
    {
        Shared.View = this;

        Shared.CommandResponse = new CommandResponse(
            "Input your authentication details to log in.",
            CommandResponse.ResponseType.Info
        );

        while (_currentStage != Stage.LoginSuccess)
        {
            Shared.RefreshView();

            switch (_currentStage)
            {
                case Stage.UsernameInput:
                    HandleUsernameInput();
                    break;
                case Stage.UsernameVerification:
                    HandleUsernameVerification();
                    break;
                case Stage.PasswordInput:
                    HandlePasswordInput();
                    break;
                case Stage.LoginAttempt:
                    HandleLoginAttempt();
                    break;
                case Stage.LoginSuccess:
                case Stage.LoginAborted:
                default:
                    return;
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("ALMS EMPLOYEE LOGIN", ConsoleColor.DarkCyan);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "To finish authenticating, complete the following steps:",
            ConsoleColor.Yellow
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ComponentWriter.WriteStep(
            "Input a username.",
            (int)_currentStage,
            (int)Stage.UsernameInput,
            (int)Stage.PasswordInput
        );
        ComponentWriter.WriteStep(
            "Input a password.",
            (int)_currentStage,
            (int)Stage.PasswordInput,
            (int)Stage.LoginSuccess
        );
        
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("Use the :exit command to cancel logging in.", ConsoleColor.Red);

        ComponentWriter.WriteUserInput(GetPrompt());
    }

    private void HandleUsernameInput()
    {
        _submittedUsername = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedUsername)) return;
        _currentStage = Stage.UsernameVerification;
    }

    private void HandleUsernameVerification()
    {
        Shared.CommandResponse = new CommandResponse(
            "Checking username validity...",
            CommandResponse.ResponseType.Loading
        );
        Shared.RefreshView();

        var usernameExists = _submittedUsername != null && EmployeeRepository.IsUsernameTaken(_submittedUsername);

        var usernameIsGlados = _submittedUsername?.ToLower() == "glados";
        const string gladosEasterEggQuote = "Come to mommy. I made cake for you!";

        if (usernameExists)
        {
            Shared.CommandResponse = new CommandResponse(
                usernameIsGlados ? gladosEasterEggQuote : "Username is valid.",
                CommandResponse.ResponseType.Success
            );
            _currentStage = Stage.PasswordInput;
        }
        else
        {
            Shared.CommandResponse = new CommandResponse(
                usernameIsGlados ? gladosEasterEggQuote : "Employee with the submitted username doesn't exist.",
                CommandResponse.ResponseType.Error
            );
            _currentStage = Stage.UsernameInput;
        }
    }

    private void HandlePasswordInput()
    {
        _submittedPassword = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedPassword)) return;
        _currentStage = Stage.LoginAttempt;
    }

    private void HandleLoginAttempt()
    {
        Shared.CommandResponse = new CommandResponse("Authenticating...", CommandResponse.ResponseType.Loading);
        Shared.RefreshView();

        var result = Authenticator.Login(new LoginRequest(_submittedUsername ?? "", _submittedPassword ?? ""));

        switch (result)
        {
            case Authenticator.LoginResult.Success:
                _currentStage = Stage.LoginSuccess;
                Shared.CommandResponse = new CommandResponse(
                    $"Employee {Session.GetInstance().Employee?.Name} {Session.GetInstance().Employee?.Surname} successfully logged in!",
                    CommandResponse.ResponseType.Success
                );
                Shared.View = new MessagingView();
                break;

            case Authenticator.LoginResult.UserDoesNotExist:
                Shared.CommandResponse = new CommandResponse(
                    "Somehow, you don't exist anymore.", CommandResponse.ResponseType.Error
                );
                _currentStage = Stage.UsernameInput;
                break;

            case Authenticator.LoginResult.IncorrectPassword:
                Shared.CommandResponse = new CommandResponse(
                    "Incorrect password.", CommandResponse.ResponseType.Error
                );
                _currentStage = Stage.PasswordInput;
                break;
        }
    }

    private string GetPrompt()
    {
        return _currentStage switch
        {
            Stage.UsernameInput => "Username:",
            Stage.PasswordInput => "Password:",
            _ => ""
        };
    }

    private bool CheckForExitCommand(string userInput)
    {
        var result = CommandProcessor.InvokeCommand(userInput, Commands);
        if (result != CommandProcessor.Result.Success) return false;
        
        _currentStage = Stage.LoginAborted;
        return true;
    }
}