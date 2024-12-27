using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customer.GetCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Customers;

public class GetCustomerRequestProfile : Profile
{
    public GetCustomerRequestProfile()
    {
        CreateMap<GetCustomerRequest, GetCustomerCommand>();
    }
}