
using userJwtApp.Models.UserModel;

public class UserReadModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public static UserReadModel FromUserModel(UserModel userModel) => new()
    {
        FirstName = userModel.FirstName,
        LastName = userModel.LastName,
        UserName = userModel.UserName,
    };
}