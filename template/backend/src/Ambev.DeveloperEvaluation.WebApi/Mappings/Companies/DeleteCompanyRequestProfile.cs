using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Company.DeleteCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Companies;

public class DeleteCompanyRequestProfile : Profile
{
    public DeleteCompanyRequestProfile()
    {
        CreateMap<DeleteCompanyRequest, DeleteCompanyCommand>();
    }
}