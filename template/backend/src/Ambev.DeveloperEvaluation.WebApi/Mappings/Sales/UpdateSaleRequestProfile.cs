using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Sales;

public class UpdateSaleRequestProfile : Profile
{
    public UpdateSaleRequestProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
    }
}