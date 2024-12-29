using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleRequestProfile : Profile
{
    public GetSaleRequestProfile()
    {
        CreateMap<GetSaleRequest, GetSaleQuery>();
        
        CreateMap<Guid, GetSaleQuery>()
            .ConstructUsing(id => new GetSaleQuery(id));
    }
}