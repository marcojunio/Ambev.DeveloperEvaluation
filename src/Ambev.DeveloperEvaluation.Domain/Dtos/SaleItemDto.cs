namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public sealed record SaleItemDto(Guid ProductId,int Quantity,decimal UnitPrice);