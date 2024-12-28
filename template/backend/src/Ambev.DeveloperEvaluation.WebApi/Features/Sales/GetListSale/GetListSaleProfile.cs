using Ambev.DeveloperEvaluation.Application.Sale.GetListSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSale;

public class GetListSaleProfile : Profile
{
    public GetListSaleProfile()
    {
        CreateMap<GetListSaleRequest, GetListSaleQuery>();
    }
}