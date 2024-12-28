using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Domain.Entities.Sale>()
            .ForMember(
                dest => dest.Items, opt => 
                opt.MapFrom(src => src.SaleItems))
            .ReverseMap();

        CreateMap<SaleItemDto, SaleItem>().ReverseMap();

        CreateMap<Domain.Entities.Sale, CreateSaleResult>().ReverseMap();
    }
}