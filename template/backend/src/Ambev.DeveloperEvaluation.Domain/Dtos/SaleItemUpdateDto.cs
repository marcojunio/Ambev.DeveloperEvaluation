namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public sealed record SaleItemUpdateDto(Guid? Id,string ProductId,int Quantity);