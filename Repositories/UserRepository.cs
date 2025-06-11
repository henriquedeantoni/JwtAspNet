using Microsoft.EntityFrameworkCore;
using userJwtApp.Models.UserModel;
using userJwtApp.Repositories;
using userJwtApp.Repositories.Contexts;

namespace userJwtApp.Repositories;
public class UserRepository : IUserInterface
{
    private DatabaseContext dbContext { get; }

    public UserRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<UserModel> RegisterUser(UserModel user) =>
        (await dbContext.User.AddAsync(user)).Entity;


    public async Task<UserModel> GetUserById(Guid userId) =>
        (await dbContext.User.FindAsync(userId));

    public async Task<UserModel> GetUserByUserName(string username) =>
        await dbContext.User.FirstOrDefaultAsync(user => user.Username == username);

    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
}