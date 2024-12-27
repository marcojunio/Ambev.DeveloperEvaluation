using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Companies;

public class CreateCompanyRequestProfile : Profile
{
    public CreateCompanyRequestProfile()
    {
        CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
    }
}