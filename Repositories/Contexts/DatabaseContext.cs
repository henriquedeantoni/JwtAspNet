using Microsoft.EntityFrameworkCore;
using userJwtApp.Models.ProductModel;
using userJwtApp.Models.UserModel;

namespace userJwtApp.Respositories.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<ProductModel> product { get; set; } = null!;
    public DbSet<UserModel> user { get; set; } = null!;
}