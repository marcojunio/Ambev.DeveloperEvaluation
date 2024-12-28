using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.GetCompany;

public class GetCompanyProfile : Profile
{
    public GetCompanyProfile()
    {
        CreateMap<Guid, GetCompanyQuery>()
            .ConstructUsing(id => new GetCompanyQuery(id));
    }
}