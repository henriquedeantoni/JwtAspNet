using Microsoft.EntityFrameworkCore;
using userJwtApp.Models.UserModel;
using userJwtApp.Respositories;
using userJwtApp.Respositories.Contexts;

public class UserRepository : IUserInterface
{
    private DatabaseContext dbContext { get; }

    public UserRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<UserModel> RegisterUser(UserModel user) =>
        (await dbContext.user.AddAsync(user)).Entity;


    public async Task<UserModel> GetUserById(int userId) =>
        (await dbContext.user.FindAsync(userId));

    public async Task<UserModel> GetUserByUserName(string username) =>
        await dbContext.user.FirstOrDefaultAsync(user => user.UserName == username);

    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
}