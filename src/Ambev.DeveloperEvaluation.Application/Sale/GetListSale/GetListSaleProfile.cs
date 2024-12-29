using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetListSale;

public class GetListSaleProfile : Profile
{
    public GetListSaleProfile()
    {
        CreateMap<Domain.Entities.Sale, GetListSaleResult>()
            .ForMember(d => d.SaleItems, member => member.MapFrom(f => f.Items));

        CreateMap<SaleItem, SaleItemResultDto>();
    }
}