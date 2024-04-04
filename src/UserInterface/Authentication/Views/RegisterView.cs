using ApertureMessenger.AlmsConnection.Authentication;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.Logic.ValidityCheckers;
using ApertureMessenger.UserInterface.Authentication.Commands;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.Views;

public class RegisterView : IView
{
    private static readonly ICommand[] Commands =
    [
        new Exit()
    ];
    
    private enum Stage
    {
        UsernameInput,
        UsernameVerification,
        NameInput,
        NameVerification,
        SurnameInput,
        SurnameVerification,
        PasswordInput,
        SecondPasswordInput,
        PasswordVerification,
        RegisterAttempt,
        RegisterSuccess,
        RegisterAborted,
    }

    private Stage _currentStage = Stage.UsernameInput;

    private string? _submittedUsername;
    private string? _submittedName;
    private string? _submittedSurname;
    private string? _submittedPassword;
    private string? _submittedSecondPassword;

    public void Process()
    {
        Shared.CommandResponse = new CommandResponse("Follow the required steps to register a new account.",
            CommandResponse.ResponseType.Info);

        while (_currentStage != Stage.RegisterSuccess)
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
                case Stage.NameInput:
                    HandleNameInput();
                    break;
                case Stage.NameVerification:
                    HandleNameVerification();
                    break;
                case Stage.SurnameInput:
                    HandleSurnameInput();
                    break;
                case Stage.SurnameVerification:
                    HandleSurnameVerification();
                    break;
                case Stage.PasswordInput:
                    HandlePasswordInput();
                    break;
                case Stage.SecondPasswordInput:
                    HandleSecondPasswordInput();
                    break;
                case Stage.PasswordVerification:
                    HandlePasswordVerification();
                    break;
                case Stage.RegisterAttempt:
                    HandleRegisterAttempt();
                    break;
                case Stage.RegisterSuccess:
                case Stage.RegisterAborted:
                default:
                    return;
            }
        }
    }
    
    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("CREATING A NEW ALMS EMPLOYEE ACCOUNT", ConsoleColor.DarkYellow);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "To create a new ALMS employee account, complete the following steps:",
            ConsoleColor.Yellow
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ComponentWriter.WriteStep(
            "Input a username.",
            (int)_currentStage,
            (int)Stage.UsernameInput,
            (int)Stage.NameInput
        );
        ComponentWriter.WriteStep(
            "Input your first name.",
            (int)_currentStage,
            (int)Stage.NameInput,
            (int)Stage.SurnameInput
        );
        ComponentWriter.WriteStep(
            "Input your surname.",
            (int)_currentStage,
            (int)Stage.SurnameInput,
            (int)Stage.PasswordInput
        );
        ComponentWriter.WriteStep(
            "Input a password.",
            (int)_currentStage,
            (int)Stage.PasswordInput,
            (int)Stage.SecondPasswordInput
        );
        ComponentWriter.WriteStep(
            "Input the password again for verification.",
            (int)_currentStage,
            (int)Stage.SecondPasswordInput,
            (int)Stage.RegisterSuccess
        );
        
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteWithWordWrap("Use the :exit command to cancel the registration.", ConsoleColor.Red);

        ComponentWriter.WriteUserInput(GetPrompt(), _currentStage is Stage.PasswordInput or Stage.SecondPasswordInput);
    }

    private void HandleUsernameInput()
    {
        _submittedUsername = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedUsername)) return;
        _currentStage = Stage.UsernameVerification;
    }

    private void HandleUsernameVerification()
    {
        var result = UsernameValidityChecker.CheckUsername(_submittedUsername ?? "");
        if (result != UsernameValidityChecker.Result.Ok)
        {
            Shared.CommandResponse = result switch
            {
                UsernameValidityChecker.Result.InvalidCharacters => new CommandResponse(
                    "Only English letters and numbers are allowed for usernames.", CommandResponse.ResponseType.Error),
                UsernameValidityChecker.Result.TooShort => new CommandResponse(
                    "Username must be at least 2 characters long.", CommandResponse.ResponseType.Error),
                UsernameValidityChecker.Result.TooLong => new CommandResponse(
                    "Username must be shorter than 32 characters.", CommandResponse.ResponseType.Error),
                UsernameValidityChecker.Result.Taken => new CommandResponse(
                    "Username is already taken.", CommandResponse.ResponseType.Error),
                _ => Shared.CommandResponse
            };

            _currentStage = Stage.UsernameInput;
            return;
        }

        Shared.CommandResponse = new CommandResponse("Username OK.", CommandResponse.ResponseType.Success);
        _currentStage = Stage.NameInput;
    }

    private void HandleNameInput()
    {
        _submittedName = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedName)) return;
        _currentStage = Stage.NameVerification;
    }

    private void HandleNameVerification()
    {
        var result = NameValidityChecker.CheckName(_submittedName ?? "");
        if (result != NameValidityChecker.Result.Ok)
        {
            Shared.CommandResponse = result switch
            {
                NameValidityChecker.Result.TooShort => new CommandResponse(
                    "Name must be at least 2 characters long.", CommandResponse.ResponseType.Error),
                NameValidityChecker.Result.TooLong => new CommandResponse(
                    "Name must be shorter than 32 characters.", CommandResponse.ResponseType.Error),
                _ => Shared.CommandResponse
            };

            _currentStage = Stage.NameInput;
            return;
        }

        Shared.CommandResponse = new CommandResponse("Name OK.", CommandResponse.ResponseType.Success);
        _currentStage = Stage.SurnameInput;
    }

    private void HandleSurnameInput()
    {
        _submittedSurname = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedSurname)) return;
        _currentStage = Stage.SurnameVerification;
    }

    private void HandleSurnameVerification()
    {
        var result = NameValidityChecker.CheckName(_submittedSurname ?? "");
        if (result != NameValidityChecker.Result.Ok)
        {
            Shared.CommandResponse = result switch
            {
                NameValidityChecker.Result.TooShort => new CommandResponse(
                    "Surname must be at least 2 characters long.", CommandResponse.ResponseType.Error),
                NameValidityChecker.Result.TooLong => new CommandResponse(
                    "Surname must be shorter than 32 characters.", CommandResponse.ResponseType.Error),
                _ => Shared.CommandResponse
            };

            _currentStage = Stage.SurnameInput;
            return;
        }

        Shared.CommandResponse = new CommandResponse(
            "Surname OK.",
            CommandResponse.ResponseType.Success
        );
        _currentStage = Stage.PasswordInput;
    }

    private void HandlePasswordInput()
    {
        _submittedPassword = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedPassword)) return;
        _currentStage = Stage.SecondPasswordInput;
    }

    private void HandleSecondPasswordInput()
    {
        Shared.CommandResponse = new CommandResponse(
            "Type the same password again for verification.", CommandResponse.ResponseType.Info);
        Shared.RefreshView();
        
        _submittedSecondPassword = ConsoleReader.ReadCommandFromUser();
        if (CheckForExitCommand(_submittedSecondPassword)) return;
        _currentStage = Stage.PasswordVerification;
    }

    private void HandlePasswordVerification()
    {
        if (_submittedPassword != _submittedSecondPassword)
        {
            Shared.CommandResponse = new CommandResponse(
                "Passwords do not match.", CommandResponse.ResponseType.Error);
            _currentStage = Stage.PasswordInput;
            return;
        }

        var result = PasswordValidityChecker.CheckPassword(_submittedPassword ?? "");
        if (result != PasswordValidityChecker.Result.Ok)
        {
            Shared.CommandResponse = result switch
            {
                PasswordValidityChecker.Result.TooShort => new CommandResponse(
                    "Password must be at least 8 characters long.", CommandResponse.ResponseType.Error),
                PasswordValidityChecker.Result.TooLong => new CommandResponse(
                    "Password must be shorter than 49 characters.", CommandResponse.ResponseType.Error),
                _ => Shared.CommandResponse
            };

            _currentStage = Stage.PasswordInput;
            return;
        }

        _currentStage = Stage.RegisterAttempt;
    }

    private void HandleRegisterAttempt()
    {
        if (_submittedUsername == null ||
            _submittedName == null ||
            _submittedSurname == null ||
            _submittedPassword == null
           )
        {
            Shared.CommandResponse = new CommandResponse(
                "Employee details were not properly submitted.", CommandResponse.ResponseType.Error
            );
            _currentStage = Stage.UsernameInput;
            return;
        }

        var result = EmployeeCreator.Register(
            new RegisterRequest(_submittedUsername, _submittedName, _submittedSurname, _submittedPassword));

        if (result == EmployeeCreator.RegisterResult.Success)
        {
            Shared.CommandResponse = new CommandResponse(
                "Registration successful. You can now log in.", CommandResponse.ResponseType.Success
            );
            _currentStage = Stage.RegisterSuccess;
            Shared.View = new AuthenticationView();
        }
        else {
            Shared.CommandResponse = new CommandResponse(
                "Employee details were rejected by ALMS.", CommandResponse.ResponseType.Error
            );
            _currentStage = Stage.UsernameInput;
        }
    }

    private string GetPrompt()
    {
        return _currentStage switch
        {
            Stage.UsernameInput => "Username:",
            Stage.NameInput => "Name:",
            Stage.SurnameInput => "Surname:",
            Stage.PasswordInput => "Password:",
            Stage.SecondPasswordInput => "Password again:",
            _ => ""
        };
    }
    
    private bool CheckForExitCommand(string userInput)
    {
        var result = CommandProcessor.InvokeCommand(userInput, Commands);
        if (result != CommandProcessor.Result.Success) return false;
        
        _currentStage = Stage.RegisterAborted;
        return true;
    }
}