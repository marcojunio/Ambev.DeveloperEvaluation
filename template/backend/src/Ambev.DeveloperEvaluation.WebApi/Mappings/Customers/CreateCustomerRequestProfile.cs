using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customer.CreateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Customers;

public class CreateCustomerRequestProfile : Profile
{
    public CreateCustomerRequestProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();
    }
}