using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleRequestProfile : Profile
{
    public CancelSaleRequestProfile()
    {
        CreateMap<CancelSaleRequest, CancelSaleCommand>();
        
        CreateMap<Guid, CancelSaleCommand>()
            .ConstructUsing(id => new CancelSaleCommand(id));
    }
}