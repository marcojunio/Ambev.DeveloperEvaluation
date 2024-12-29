namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public sealed record SaleItemResultDto(Guid Id,Guid ProductId,int Quantity,decimal Discount,decimal UnitPrice);