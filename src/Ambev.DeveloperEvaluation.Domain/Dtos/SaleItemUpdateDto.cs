namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public sealed record SaleItemUpdateDto(Guid? Id,Guid ProductId,int Quantity,decimal UnitPrice);