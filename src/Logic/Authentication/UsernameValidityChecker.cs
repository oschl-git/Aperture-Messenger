namespace ApertureMessenger.Logic.Authentication;

public static class UsernameValidityChecker
{
    public enum Response {
        Valid,
        Taken,
        InvalidCharacters,
        TooLong,
    }
    
    public static void CheckValidity(string username)
    {
        throw new NotImplementedException();
    }
}