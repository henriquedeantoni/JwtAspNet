namespace userJwtApp.Exceptions;

public class UserAllreadyRegisteredException : Exception
{
    private const string EXCEPTION_MESSAGE = "User with username {username} allready registered";

    public UserAllreadyRegisteredException(string username) : base(string.Format(EXCEPTION_MESSAGE, username))
    {    }
}