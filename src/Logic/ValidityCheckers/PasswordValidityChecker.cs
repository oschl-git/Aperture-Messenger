namespace ApertureMessenger.Logic.ValidityCheckers;

/// <summary>
/// Allows checking if passwords are valid.
/// </summary>
public static class PasswordValidityChecker
{
    public enum Result
    {
        Ok,
        TooShort,
        TooLong
    }

    public static Result CheckPassword(string password)
    {
        if (password.Length < 8) return Result.TooShort;

        if (password.Length > 48) return Result.TooLong;

        return Result.Ok;
    }
}