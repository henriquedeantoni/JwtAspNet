using userJwtApp.Models.UserModels;

namespace userJwtApp.Models.Controllers;

public interface IUserController
{
    /// <summary>
    /// Sign new user
    /// </summary>
    /// <param name="signRequest"></param>
    /// <returns></returns>
    public Task<string> SignUser(UserSignRequestModel signRequest);

    /// <summary>
    /// User login
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    public Task<string> LoginUser(UserLoginRequestModel loginRequest);
}
