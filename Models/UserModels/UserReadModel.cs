
using userJwtApp.Models.UserModels;

public class UserReadModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public bool Active { get; init; }
    public static UserReadModel FromUserModel(UserModel userModel) => new()
    {
        FirstName = userModel.FirstName,
        LastName = userModel.LastName,
        Username = userModel.Username,
        Active = userModel.Active,
    };
}