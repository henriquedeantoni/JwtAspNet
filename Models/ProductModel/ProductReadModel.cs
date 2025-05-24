using userJwtApp.Models.ProductModel;


public class ProductReadModel
{
    public int Id { get; init; }
    public string ProductName { get; init; }
    public string Serial { get; init; }
    public decimal Price { get; init; }
    public static ProductReadModel FromProductModel(ProductModel productModel) => new()
    {
        Id = productModel.Id,
        ProductName = productModel.ProductName,
        Serial = productModel.Serial,
        Price = productModel.Price
    };
}