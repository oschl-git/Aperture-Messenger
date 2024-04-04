namespace ApertureMessenger.Logic.ValidityCheckers;

/// <summary>
/// Allows checking if names and surnames are valid.
/// </summary>
public static class NameValidityChecker
{
    public enum Result
    {
        Ok,
        TooShort,
        TooLong
    }

    public static Result CheckName(string name)
    {
        return name.Length switch
        {
            < 2 => Result.TooShort,
            > 32 => Result.TooLong,
            _ => Result.Ok
        };
    }
}