using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.GetCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Companies;

public class GetCompanyRequestProfile : Profile
{
    public GetCompanyRequestProfile()
    {
        CreateMap<GetCompanyRequest, GetCompanyCommand>();
    }
}