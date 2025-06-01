namespace userJwtApp.Exceptions;

public class UserNotFoundException : System.Exception
{
    private const string EXCEPTION_MESSAGE = "User with Username[{0}] was not found";
    
    public UserNotFoundException(string username) : 
        base(string.Format(EXCEPTION_MESSAGE, username))
    { }
}