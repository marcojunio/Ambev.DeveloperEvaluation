using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.GetCustomer;

public class GetCustomerProfile : Profile
{
    public GetCustomerProfile()
    {
        CreateMap<Guid, GetCustomerQuery>()
            .ConstructUsing(id => new GetCustomerQuery(id));
    }
}