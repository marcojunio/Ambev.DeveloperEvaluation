using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestProfile : Profile
{
    public UpdateSaleRequestProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
    }
}