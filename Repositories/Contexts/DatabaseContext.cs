using Microsoft.EntityFrameworkCore;
using userJwtApp.Models.ProductModel;
using userJwtApp.Models.UserModel;

namespace userJwtApp.Repositories.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<ProductModel> Product { get; set; } = null!;
    public DbSet<UserModel> User { get; set; } = null!;
}