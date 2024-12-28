using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, GetSaleResult>()
            .ForMember(d => d.SaleItems, member => member.MapFrom(f => f.Items))
            .ForMember(d => d.CustomerName, member => member.MapFrom(f => f.Customer.Name));

        CreateMap<SaleItem,SaleItemResultDto>()
            .ForMember(d => d.ProductName, member => member.MapFrom(f => f.Product.Name));
    }
}