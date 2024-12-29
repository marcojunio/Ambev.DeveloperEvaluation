using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Domain.Entities.Sale>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.SaleItems));

        CreateMap<SaleItemUpdateDto, SaleItem>();

        CreateMap<Domain.Entities.Sale, UpdateSaleResult>()
            .ForMember(d => d.SaleItems, member => member.MapFrom(f => f.Items));
    }
}