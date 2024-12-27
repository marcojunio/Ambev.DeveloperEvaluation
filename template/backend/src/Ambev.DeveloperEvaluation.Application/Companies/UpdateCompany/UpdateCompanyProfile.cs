using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;

public class UpdateCompanyProfile : Profile
{
    public UpdateCompanyProfile()
    {
        CreateMap<UpdateCompanyCommand, Company>();
        CreateMap<Company, UpdateCompanyResult>();
    }
}