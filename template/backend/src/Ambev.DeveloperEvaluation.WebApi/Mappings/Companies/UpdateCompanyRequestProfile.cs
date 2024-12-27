using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.UpdateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Companies;

public class UpdateCompanyRequestProfile : Profile
{
    public UpdateCompanyRequestProfile()
    {
        CreateMap<UpdateCompanyRequest, UpdateCompanyCommand>();
    }
}