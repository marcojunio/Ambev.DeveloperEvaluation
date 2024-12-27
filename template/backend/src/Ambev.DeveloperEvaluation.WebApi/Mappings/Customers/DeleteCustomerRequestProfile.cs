using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customer.DeleteCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Customers;

public class DeleteCustomerRequestProfile : Profile
{
    public DeleteCustomerRequestProfile()
    {
        CreateMap<DeleteCustomerRequest, DeleteCustomerCommand>();
    }
}