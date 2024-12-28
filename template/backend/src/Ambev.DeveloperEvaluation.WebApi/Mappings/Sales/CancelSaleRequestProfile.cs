using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

public class CancelSaleRequestProfile : Profile
{
    public CancelSaleRequestProfile()
    {
        CreateMap<CancelSaleRequest, CancelSaleCommand>();
    }
}