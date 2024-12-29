using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

public class GetSaleRequestProfile : Profile
{
    public GetSaleRequestProfile()
    {
        CreateMap<GetSaleRequest, GetSaleQuery>();
    }
}