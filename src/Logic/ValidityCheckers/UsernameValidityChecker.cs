using System.Text.RegularExpressions;
using ApertureMessenger.AlmsConnection.Repositories;

namespace ApertureMessenger.Logic.ValidityCheckers;

/// <summary>
/// Allows checking if usernames are valid.
/// </summary>
public static class UsernameValidityChecker
{
    public enum Result
    {
        Ok,
        InvalidCharacters,
        TooShort,
        TooLong,
        Taken
    }

    public static Result CheckUsername(string username)
    {
        const string pattern = "^[A-Za-z0-9]+$";
        if (!Regex.IsMatch(username, pattern)) return Result.InvalidCharacters;

        if (username.Length < 2) return Result.TooShort;

        if (username.Length > 32) return Result.TooLong;

        if (EmployeeRepository.IsUsernameTaken(username)) return Result.Taken;

        return Result.Ok;
    }
}