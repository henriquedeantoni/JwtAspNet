using Microsoft.EntityFrameworkCore;
using userJwtApp.Models.ProductModels;
using userJwtApp.Models.UserModels;

namespace userJwtApp.Repositories.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<ProductModel> Product { get; set; } = null!;

    public DbSet<UserModel> User { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}