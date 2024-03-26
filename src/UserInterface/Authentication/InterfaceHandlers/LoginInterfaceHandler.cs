using ApertureMessenger.AlmsConnection.Repositories;
using ApertureMessenger.AlmsConnection.Requests;
using ApertureMessenger.UserInterface.Console;
using ApertureMessenger.UserInterface.Interfaces;
using ApertureMessenger.UserInterface.Objects;

namespace ApertureMessenger.UserInterface.Authentication.InterfaceHandlers;

public class LoginInterfaceHandler : IInterfaceHandler
{
    private enum Stage
    {
        UsernameInput,
        UsernameVerification,
        PasswordInput,
        LoginAttempt,
        LoginSuccess,
    }

    private Stage currentStage = Stage.UsernameInput;

    private string? submittedUsername = null;
    private string? submittedPassword = null;

    public void Process()
    {
        SharedData.InterfaceHandler = this;

        SharedData.CommandResponse = new CommandResponse(
            "Input your authentication details to log in.",
            CommandResponse.ResponseType.Info
        );

        while (currentStage != Stage.LoginSuccess)
        {
            DrawUserInterface();

            switch (currentStage)
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
            }
        }
    }

    public void DrawUserInterface()
    {
        ConsoleWriter.Clear();

        ComponentWriter.WriteHeader("ALMS EMPLOYEE LOGIN", ConsoleColor.DarkBlue);
        ConsoleWriter.WriteLine();

        ConsoleWriter.WriteWithWordWrap(
            "To finish authenticating, complete the following steps:",
            ConsoleColor.Yellow
        );
        ConsoleWriter.WriteLine();
        ConsoleWriter.WriteLine();

        ComponentWriter.WriteStep("Input a username.", GetUsernameStepState());
        ComponentWriter.WriteStep("Input a password.", GetPasswordStepState());

        ComponentWriter.WriteUserInput(GetPrompt());
    }

    private void HandleUsernameInput()
    {
        submittedUsername = ConsoleReader.ReadCommandFromUser();
        currentStage = Stage.UsernameVerification;
    }

    private void HandleUsernameVerification()
    {
        SharedData.CommandResponse =
            new CommandResponse("Checking username validity...", CommandResponse.ResponseType.Warning);
        DrawUserInterface();

        var usernameExists = submittedUsername != null && EmployeeRepository.IsUsernameTaken(submittedUsername);

        if (usernameExists)
        {
            SharedData.CommandResponse = new CommandResponse(
                "Username is valid.",
                CommandResponse.ResponseType.Success
            );
            currentStage = Stage.PasswordInput;
        }
        else
        {
            SharedData.CommandResponse = new CommandResponse(
                "Employee with the submitted username doesn't exist.",
                CommandResponse.ResponseType.Error
            );
            currentStage = Stage.UsernameInput;
        }
    }

    private void HandlePasswordInput()
    {
        submittedPassword = ConsoleReader.ReadCommandFromUser();
        currentStage = Stage.LoginAttempt;
    }

    private void HandleLoginAttempt()
    {
        
    }

    private string GetPrompt()
    {
        return currentStage switch
        {
            Stage.UsernameInput => "Username:",
            Stage.PasswordInput => "Password:",
            _ => ""
        };
    }

    private ComponentWriter.StepStates GetUsernameStepState()
    {
        if (currentStage == Stage.UsernameInput)
        {
            return ComponentWriter.StepStates.Started;
        }
        
        if ((int)currentStage >= 2)
        {
            return ComponentWriter.StepStates.Completed;
        }

        return ComponentWriter.StepStates.Scheduled;
    }
    
    private ComponentWriter.StepStates GetPasswordStepState()
    {
        if (currentStage == Stage.PasswordInput)
        {
            return ComponentWriter.StepStates.Started;
        }
        
        if ((int)currentStage >= 4)
        {
            return ComponentWriter.StepStates.Completed;
        }

        return ComponentWriter.StepStates.Scheduled;
    }

}