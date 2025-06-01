using Microsoft.EntityFrameworkCore;
using userJwtApp.Repositories;
using userJwtApp.Repositories.Contexts;
using userJwtApp.Models.ProductModel;

namespace userJwtApp.Repositories;
public class ProductRepository : IProductRepository
{
    private DatabaseContext dbContext { get; }

    public ProductRepository(DatabaseContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ProductModel> RegisterProduct(ProductModel product) =>
        (await dbContext.Product.AddAsync(product)).Entity;

    public async Task<ProductModel?> GetProductById(int productId) =>
        await dbContext.Product.FindAsync(productId);

    public async Task<IReadOnlyList<ProductModel>> GetUserRelatedProducts(int userId) =>
        await dbContext.Product
            .Where(product => product.CreatedBy.Id == userId)
            .OrderBy(product => product.Id)
            .ToListAsync();
    public ProductModel DeleteProduct(ProductModel productModel) =>
        (dbContext.Product.Remove(productModel)).Entity;
    public async Task FlushChanges() =>
        await dbContext.SaveChangesAsync();
} 