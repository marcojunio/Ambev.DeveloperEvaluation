using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SellingCompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public IEnumerable<SaleItemUpdateDto> SaleItems { get; set; } = new List<SaleItemUpdateDto>();

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}