namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public sealed record SaleItemResultDto(Guid Id,int Quantity,decimal Discount,decimal UnitPrice);