using userJwtApp.Models.ProductModels;


public class ProductReadModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ProductName { get; init; }
    public string Serial { get; init; }
    public decimal Price { get; init; }
    public bool Active { get; init; }
    public DateTime CreatedAt { get; set; }
    public static ProductReadModel FromProductModel(ProductModel productModel) => new()
    {
        Id = productModel.Id,
        ProductName = productModel.ProductName,
        Serial = productModel.Serial,
        Price = productModel.Price,
        Active = productModel.Active,
        CreatedAt = productModel.CreatedAt,
    };
}