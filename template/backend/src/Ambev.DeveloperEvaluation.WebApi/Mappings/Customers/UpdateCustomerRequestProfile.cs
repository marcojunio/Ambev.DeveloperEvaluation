using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customer.UpdateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Customers;

public class UpdateCustomerRequestProfile : Profile
{
    public UpdateCustomerRequestProfile()
    {
        CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
    }
}