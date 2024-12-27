using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.DeleteCompany;

public class DeleteCompanyProfile : Profile
{
    public DeleteCompanyProfile()
    {
        CreateMap<Guid, DeleteCompanyCommand>()
            .ConstructUsing(id => new DeleteCompanyCommand(id));
    }
}