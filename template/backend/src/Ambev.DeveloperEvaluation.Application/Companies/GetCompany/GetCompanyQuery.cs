using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

public record GetCompanyQuery : IRequest<GetCompanyResult>
{
    public Guid Id { get; }
    
    public GetCompanyQuery(Guid id)
    {
        Id = id;
    }

    public GetCompanyQuery()
    {
        
    }
}
