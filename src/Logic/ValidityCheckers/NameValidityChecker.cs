namespace ApertureMessenger.Logic.ValidityCheckers;

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
        if (name.Length < 2)
        {
            return Result.TooShort;
        }

        if (name.Length > 32)
        {
            return Result.TooLong;
        }

        return Result.Ok;
    }
}