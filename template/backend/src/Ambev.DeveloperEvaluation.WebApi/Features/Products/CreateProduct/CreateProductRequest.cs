namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; } = 0;
    public int StockQuantity { get; set; }
    public Guid UserId { get; set; }
}