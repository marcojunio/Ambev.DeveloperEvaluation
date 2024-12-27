using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.DeleteCustomer;

public class DeleteCustomerProfile : Profile
{
    public DeleteCustomerProfile()
    {
        CreateMap<Guid, DeleteCustomerCommand>()
            .ConstructUsing(id => new DeleteCustomerCommand(id));
    }
}