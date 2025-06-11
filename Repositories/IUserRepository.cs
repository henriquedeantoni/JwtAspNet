using userJwtApp.Models.UserModel;

namespace userJwtApp.Repositories;
public interface IUserInterface
{
    /// <summary>
    /// New User Register
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<UserModel> RegisterUser(UserModel user);
    /// <summary>
    /// Get User data by Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<UserModel?> GetUserById(Guid userId);
    /// <summary>
    /// Get User data by user name
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task<UserModel?> GetUserByUserName(string username);
    /// <summary>
    /// Apply all changes to the database
    /// </summary>
    /// <returns></returns>
    public Task FlushChanges();
}